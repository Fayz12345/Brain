# Brain to Renew - Data Migration Plan

---

## 1. Scope

Migrate all device-level records from the Brain ERP (SQL Server) to the Renew ERP. Each row represents one device identified by ESN.

### Target Fields

| # | Field | Brain Source Table | Brain Column / Join Path | Notes |
|---|-------|--------------------|--------------------------|-------|
| 1 | **ESN** | `ReceiveDetail` | `ESN` | Primary device identifier (IMEI / serial) |
| 2 | **Manufacturer** | `ReceiveDetail` | `Manufacturer` (denormalized text) | Also resolvable via `ManufacturerID` -> `Option.OptionText` where Question.Name = dropdown question |
| 3 | **Model** | `ReceiveDetail` | `Model` (denormalized text) | Also resolvable via `ModelID` -> `Option.OptionText` |
| 4 | **Colour** | `ReceiveDetail` | `Colour` (denormalized text) | Also resolvable via `ColourID` -> `Option.OptionText` |
| 5 | **Receive Date** | `ReceiveDetail` | `CreateDate` | The date the device was received into Brain |
| 6 | **Shipping Created Date** | `ReceiveDetailProcessLog` | `CreateDate` WHERE Process.Name IN ('Shipped', 'Shipping') | Timestamp of when the device entered a shipping process step |
| 7 | **Grade** | `ReceiveDetail` | `Grade` (denormalized text) | Also resolvable via `GradeID` -> `Option.OptionText`. Note: flat tables also track `Received_Grade` (original) vs `Grade` (current) |
| 8 | **Vendor** | `ReceiveDetailItem` -> `Option` -> `Question` | `Option.OptionText` WHERE `Question.Name = 'Vendor'` (or similar) and `ReceiveDetailItem.Value = '1'` and `Version = 0` | Stored as a QC dropdown answer, **not** a direct column - needs verification (see Questions below) |
| 9 | **Ship To** | `ReceiveDetailItem` -> `Option` -> `Question` | `Option.OptionText` WHERE `Question.Name = 'ShipTo'` and `ReceiveDetailItem.Value = '1'` and `Version = 0` | Also a QC dropdown answer. For Sales Orders, stored on `SOCompany` with `CompanyType = 'ShipTo'` |
| 10 | **PSlip** | `ReceiveDetail` | `ProjectTag` | The packing slip / project tag identifier assigned at receiving |

---

## 2. Source Data Considerations

### 2.1 Existing Flat Tables (Potential Shortcut)

Brain already has two denormalized reporting tables that contain most of these fields:

- **`ReportingInventoryFlat`** - In-stock devices only (Version = '000'), ~41K rows, refreshed hourly
- **`ReportingTelusFlat`** - All Telus project devices across all versions, ~466K rows, refreshed nightly

These tables already join out: ESN, Manufacturer, Model, Colour, Grade, Received_Grade, ReceiveDate, Shipped_Created. However:
- They do **not** include Vendor, Ship To, or PSlip (ProjectTag)
- `ReportingInventoryFlat` excludes shipped devices (Version != '000')
- `ReportingTelusFlat` is limited to Telus projects only

**Recommendation:** Build the migration extract directly from the transactional tables to capture all devices across all projects and all statuses.

### 2.2 Data Quality Concerns

| Issue | Detail |
|-------|--------|
| Trailing spaces | Some `ProjectName` values have trailing spaces - use `RTRIM()` |
| QC answer versioning | `ReceiveDetailItem.Version = 0` gives current active answers; higher versions are historical |
| Dropdown vs. Numeric questions | For dropdown/radial questions, the actual text is in `Option.OptionText`, not `ReceiveDetailItem.Value` (which just stores `'1'` as a selected flag) |
| Duplicate ESNs | A device can be received multiple times (re-processed). Determine whether to migrate only the latest version or full history |
| NULL fields | Vendor and ShipTo may not be populated for all devices depending on project configuration |

---

## 3. Migration Steps

### Phase 1: Discovery & Mapping (Week 1)

1. **Get Renew's import schema** - Obtain the exact field names, data types, required vs optional fields, and accepted values from the Renew vendor
2. **Build field mapping document** - Map each Brain field to its Renew counterpart, including any value transformations needed
3. **Validate Question names in Brain** - Run queries against the `Question` table to confirm the exact `Question.Name` values for Vendor and ShipTo (these are configurable per project)
4. **Determine migration scope** - Decide:
   - All devices or only active/in-stock?
   - All projects or specific ones?
   - Historical versions or current state only?

### Phase 2: Extract Query Development (Week 2)

5. **Write the extraction SQL** - A single query joining:
   ```
   ReceiveDetail
     LEFT JOIN ReceiveDetailProcessLog (for Shipped date)
     LEFT JOIN ReceiveDetailItem + Option + Question (for Vendor)
     LEFT JOIN ReceiveDetailItem + Option + Question (for ShipTo)
   ```
6. **Validate extract against flat tables** - Cross-check the extract output against `ReportingInventoryFlat` / `ReportingTelusFlat` for fields they share (ESN, Manufacturer, Model, etc.) to verify correctness
7. **Run data profiling** - Check for NULLs, unexpected values, duplicates, character encoding issues

### Phase 3: Transform & Load Prep (Week 3)

8. **Build transformation layer** - Handle:
   - Value mapping (e.g., Brain grade codes to Renew grade codes)
   - Date format conversions
   - Trimming whitespace
   - Default values for required fields that are NULL in Brain
9. **Generate staging CSV / import file** - In the format Renew expects
10. **Renew vendor sets up import endpoint or process** - API, CSV upload, direct DB insert, etc.

### Phase 4: Testing (Week 4)

11. **Load into Renew staging/sandbox environment** - Do NOT load into production
12. **Validate record counts** - Brain extract count must match Renew imported count
13. **Spot-check individual devices** - Pick 20-30 devices across different projects and verify each field matches
14. **Test edge cases** - Devices with NULL Vendor, multiple ShipTo values, re-received ESNs

### Phase 5: Production Migration (Week 5)

15. **Freeze Brain data entry** (or agree on a cutoff date/time)
16. **Run final extract** from Brain
17. **Load into Renew production**
18. **Post-migration validation** - Full count reconciliation + spot checks
19. **Document the cutover** - Record exact timestamps, row counts, any exceptions

---

## 4. Questions for the Renew Vendor

### Import Format & Process
1. What import format does Renew accept? (CSV, JSON, API, direct SQL, other?)
2. Is there a staging/sandbox environment we can test imports against?
3. Is there a bulk import tool or API, or are we inserting row-by-row?
4. What are the rate limits or batch size limits on import?

### Field Requirements
5. What are the exact field names and data types in Renew for each of our 10 fields?
6. Which fields are required vs optional in Renew?
7. Does Renew have its own device identifier, or will it use our ESN as the primary key?
8. Are there controlled/picklist values for Manufacturer, Model, Colour, or Grade in Renew? If so, provide the valid value lists so we can map Brain's values to Renew's.
9. Does Renew expect Vendor and ShipTo as free-text, a foreign key to a Renew entity, or a picklist value?
10. What date format does Renew expect? (ISO 8601, mm/dd/yyyy, Unix timestamp?)

### Data Scope
11. Does Renew want only current-state devices, or historical records too?
12. How should we handle a device that exists in Brain multiple times (re-received, re-graded)?
13. Is there a maximum record count for a single import batch?

### Post-Migration
14. Can imported records be rolled back if validation fails?
15. Will Renew assign new internal IDs, or should we pass Brain's `ReceiveDetailID` as an external reference?
16. Is there a reconciliation report Renew can generate after import for us to validate against?

---

## 5. Questions for Internal Team

1. **Scope:** Are we migrating all projects or only specific ones (e.g., Telus only)?
2. **Scope:** All-time history or only devices from a certain date forward?
3. **Scope:** Only in-stock (Version = '000'), or shipped/closed devices too?
4. **Vendor field:** Confirm the exact `Question.Name` used for Vendor in each project - this may vary by project configuration
5. **ShipTo field:** Same as above - confirm the Question.Name. For sales-order-based shipping, ShipTo lives on `SOCompany` instead of `ReceiveDetailItem`; do we need both sources?
6. **PSlip = ProjectTag?** Confirm that "PSlip" maps to `ReceiveDetail.ProjectTag`
7. **Cutover plan:** Will both systems run in parallel, or is this a hard cutover?
8. **Downtime window:** How long can Brain be frozen for the final extract?

---

## 6. Risks

| Risk | Impact | Mitigation |
|------|--------|------------|
| Vendor/ShipTo Question names vary by project | Missing data for some projects | Audit all Question.Name values across projects before building extract |
| Renew picklist values don't match Brain's | Import rejects or data corruption | Get Renew's valid value lists early; build mapping table |
| Duplicate ESNs in extract | Over-count or wrong data in Renew | Define business rules for which record wins (latest, specific version, etc.) |
| Large dataset performance | Slow extract or import timeouts | Batch the migration; test with a subset first |
| Data entered in Brain after cutoff | Lost data | Agree on a hard freeze time; run a delta extract if needed |
