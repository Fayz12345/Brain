using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using BW_WebApp.DataManagers;


namespace BW_WebApp.Classes
{
    public class MailAttachment
    {
        #region Fields
        private MemoryStream stream;
        private string filename;
        private string mediaType;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the data stream for this attachment
        /// </summary>
        public Stream Data { get { return stream; } }
        /// <summary>
        /// Gets the original filename for this attachment
        /// </summary>
        public string Filename { get { return filename; } }
        /// <summary>
        /// Gets the attachment type: Bytes or String
        /// </summary>
        public string MediaType { get { return mediaType; } }
        /// <summary>
        /// Gets the file for this attachment (as a new attachment)
        /// </summary>
        public Attachment File { get { return new Attachment(Data, Filename, MediaType); } }
        #endregion
        #region Constructors
        /// <summary>
        /// Construct a mail attachment form a byte array
        /// </summary>
        /// <param name="data">Bytes to attach as a file</param>
        /// <param name="filename">Logical filename for attachment</param>
        public MailAttachment(byte[] data, string filename)
        {
            this.stream = new MemoryStream(data);
            this.filename = filename;
            this.mediaType = MediaTypeNames.Application.Octet;
        }
        /// <summary>
        /// Construct a mail attachment from a string
        /// </summary>
        /// <param name="data">String to attach as a file</param>
        /// <param name="filename">Logical filename for attachment</param>
        public MailAttachment(string data, string filename)
        {
            this.stream = new MemoryStream(Encoding.ASCII.GetBytes(data));
            this.filename = filename;
            this.mediaType = MediaTypeNames.Text.Html;
        }
        #endregion
    }
    public class SendEmail
    {


        public string SendESNAuthorizedEmail(string subject,
                                         string body,
                                         params MailAttachment[] attachments)
        {
            string to = System.Configuration.ConfigurationManager.AppSettings["ClientAuthorizedESNEmailAddress"];
            if (to != null && to.Length > 0)
            {
                return Email(to, body, subject, attachments);
            }
            return "Error, Check web.config, no 'ClientAuthorizedESNEmailAddress=' found";
        }





        public string SendPartsReturnedEmail(decimal ReceiveDetailID, string LocalIP, string UserName)
        {
            if (ReceiveDetailID < 0) { return "Invalid ESN Number"; }




            string subject = "";
            StringBuilder Body = new StringBuilder();
            //MailAttachment[] attachments = new MailAttachment();
            string FriendlyName = "FrendlyName";
            string ESN = "ESN";
            string MasterClient = "Master Client";
            string Carrier = "Carrier";
            string Manufacturer = "Manufacturer";
            string Model = "Model";
            string Colour = "Colour";
            string Part_01 = "";
            string Part_02 = "";
            string Part_03 = "";
            string RequestNote = "";
            //string LocalIP = "LocalIP";
            DateTime SaveTime_Date = DateTime.Now;

            ReceiveDetailManager rdm = new ReceiveDetailManager(UserName);

            using (clsLinqDataContext ctx = rdm.GetDataContext(UserName))
            {
                ReceiveDetail rd = ctx.ReceiveDetails.FirstOrDefault(x => x.ReceiveDetailID == ReceiveDetailID);
                if (rd == null) { return "ESN Not found"; }

                BasicUserUtilities buu = new BasicUserUtilities(UserName);
                FriendlyName = buu.GetThisUserFriendlyName(UserName);
                ESN = rd.ESN;
                MasterClient = rd.ClientLocation.Client.CompanyName;
                Carrier = rd.Carrier;
                Manufacturer = rd.Manufacturer;
                Model = rd.Model;
                Colour = rd.Colour;

                // Get the "NONE" Value for each of the options.

                foreach (ReceiveDetailItem i in rd.ReceiveDetailItems.Where(x => x.Option.Question.Name == "Returned Part 1" ||
                                                                                x.Option.Question.Name == "Returned Part 2" ||
                                                                                x.Option.Question.Name == "Returned Part 3" ||
                                                                                x.Option.Question.Name == "Returned Notes"))
                {
                    if (i.Option.Question.Name == "Returned Part 1")
                    {
                        Part_01 = i.Option.OptionText;
                        //if (Part_01.ToUpper() != "NONE")
                        //{
                        //    Option o = ctx.Options.FirstOrDefault(x => x.QuestionID == i.Option.QuestionID && x.OptionText.ToUpper() == "NONE");
                        //    if (o != null)
                        //    {
                        //        //decimal oID = ctx.Options.FirstOrDefault(x => x.Question.Name == "Returned Part 1" && x.OptionText.ToUpper() == "NONE").OptionID;
                        //        //i.OptionID = 6243;         //o.OptionID;
                        //    }
                        //}
                    }
                    if (i.Option.Question.Name == "Returned Part 2")
                    {
                        Part_02 = i.Option.OptionText;
                        //if (Part_02.ToUpper() != "NONE")
                        //{
                        //    Option o = ctx.Options.FirstOrDefault(x => x.QuestionID == i.Option.QuestionID && x.OptionText.ToUpper() == "NONE");
                        //    if (o != null)
                        //    {
                        //        //decimal oID = ctx.Options.FirstOrDefault(x => x.Question.Name == "Returned Part 1" && x.OptionText.ToUpper() == "NONE").OptionID;
                        //        //i.OptionID = o.OptionID;
                        //        //i.OptionID = 6272;         //o.OptionID;
                        //    }
                        //}
                    }
                    if (i.Option.Question.Name == "Returned Part 3")
                    {
                        Part_03 = i.Option.OptionText;
                        //if (Part_03.ToUpper() != "NONE")
                        //{
                        //    Option o = ctx.Options.FirstOrDefault(x => x.QuestionID == i.Option.QuestionID && x.OptionText.ToUpper() == "NONE");
                        //    if (o != null)
                        //    {
                        //        //decimal oID = ctx.Options.FirstOrDefault(x => x.Question.Name == "Returned Part 1" && x.OptionText.ToUpper() == "NONE").OptionID;
                        //        //i.OptionID = o.OptionID;
                        //        //i.OptionID = 6300;         //o.OptionID;
                        //    }
                        //}
                    }
                    if (i.Option.Question.Name == "Returned Notes")
                    {
                        RequestNote = i.Value;
                        i.Value = "";
                    }
                    ctx.SubmitChanges();
                }
                //Part_01 = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Returned Part 1");
                //Part_02 = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Returned Part 2");
                //Part_03 = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Returned Part 3");
                //RequestNote = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Request Notes");


                //MasterPartManager pm = new MasterPartManager(UserName);
                //////if (Part_01.Trim().Length > 0 && Part_01.ToUpper() != "NONE")
                //////{
                ////    //pm.AddRequestedItem(ReceiveDetailID, FriendlyName, MasterClient, rd.CarrierID, (decimal)rd.ManufacturerID, rd.ModelID, rd.ColourID, Carrier, Manufacturer, Model, Colour, Part_01, RequestNote);
                //////}
                //////if (Part_02.Trim().Length > 0 && Part_02.ToUpper() != "NONE")
                //////{
                //////    pm.AddRequestedItem(ReceiveDetailID, FriendlyName, MasterClient, rd.CarrierID, (decimal)rd.ManufacturerID, rd.ModelID, rd.ColourID, Carrier, Manufacturer, Model, Colour, Part_02, RequestNote);
                //////}
                //////if (Part_03.Trim().Length > 0 && Part_03.ToUpper() != "NONE")
                //////{
                //////    pm.AddRequestedItem(ReceiveDetailID, FriendlyName, MasterClient, rd.CarrierID, (decimal)rd.ManufacturerID, rd.ModelID, rd.ColourID, Carrier, Manufacturer, Model, Colour, Part_03, RequestNote);
                //////}


                //// The below section does not really add to anything.
                //MasterPartManager pm = new MasterPartManager(UserName);
                //if (Part_01.Trim().Length > 0 && Part_01.ToUpper() != "NONE")
                //{
                //    pm.AddReturnedItem(ReceiveDetailID,rd.IFSLocation, FriendlyName, MasterClient, rd.CarrierID, (decimal)rd.ManufacturerID, rd.ModelID, rd.ColourID, Carrier, Manufacturer, Model, Colour, Part_01, RequestNote);
                //}
                //if (Part_02.Trim().Length > 0 && Part_02.ToUpper() != "NONE")
                //{
                //    pm.AddReturnedItem(ReceiveDetailID, rd.IFSLocation, FriendlyName, MasterClient, rd.CarrierID, (decimal)rd.ManufacturerID, rd.ModelID, rd.ColourID, Carrier, Manufacturer, Model, Colour, Part_02, RequestNote);
                //}
                //if (Part_03.Trim().Length > 0 && Part_03.ToUpper() != "NONE")
                //{
                //    pm.AddReturnedItem(ReceiveDetailID, rd.IFSLocation, FriendlyName, MasterClient, rd.CarrierID, (decimal)rd.ManufacturerID, rd.ModelID, rd.ColourID, Carrier, Manufacturer, Model, Colour, Part_03, RequestNote);
                //}


                subject = "A parts Returned has been received.";


                Body.Append("<br/><br>");
                Body.Append("--------------------------------------------------------------");
                Body.Append("<br/>");

                Body.Append("Technician:  " + UserName + " (" + FriendlyName + ")");
                Body.Append("<br/><br>");
                Body.Append("ESN:  " + ESN);
                Body.Append("<br/>");
                Body.Append("MasterClient:  " + MasterClient);
                Body.Append("<br/>");
                Body.Append("<br/>");
                Body.Append("Carrier:  " + Carrier);
                Body.Append("<br/>");
                Body.Append("Manufacturer:  " + Manufacturer);
                Body.Append("<br/>");
                Body.Append("Model:  " + Model);
                Body.Append("<br/>");
                Body.Append("Colour:  " + Colour);
                Body.Append("<br/>");
                Body.Append("<br/>");

                Body.Append("Part #1: " + Part_01);
                Body.Append("<br/>");
                Body.Append("Part #2: " + Part_02);
                Body.Append("<br/>");
                Body.Append("Part #3: " + Part_03);
                Body.Append("<br/>");
                Body.Append("<br/>");

                Body.Append("Notes To Parts Department:");
                Body.Append("<br/>");
                Body.Append(RequestNote);
                Body.Append("<br/>");
                Body.Append("--------------------------------------------------------------");
                Body.Append("<br/>");
                Body.Append("<br/><br>");
                Body.Append("This email was generated with");
                Body.Append("<br/>");
                Body.Append("IMM Global Database 2.0");
                Body.Append("<br/>");
                Body.Append("https://database@gmpi.ca");
                Body.Append("<br/>");
                Body.Append("User Local IP:" + LocalIP);
                Body.Append("<br/>");
                Body.Append("Returned Time/Date:" + SaveTime_Date);
                Body.Append("<br/>");


                //rdm.UpdateESNAttribute_Blank(ctx, ReceiveDetailID, "Request Notes");
                //rdm.UpdateESNAttribute(ctx, ReceiveDetailID, "Returned Part 1", "NONE");
                //rdm.UpdateESNAttribute(ctx, ReceiveDetailID, "Returned Part 2", "NONE");
                //rdm.UpdateESNAttribute(ctx, ReceiveDetailID, "Returned Part 3", "NONE");
            }

            // Log detail to Dashboard

            CompanyDemographics company = new CompanyDemographics(UserName);
            string to = company.PartReturnEmailAddress;
            if (to != null && to.Trim().Length > 0)
            {
                return Email(to, Body.ToString(), subject);
            }
            return "";
            //return "Error, Check Company Demographics, missing Parts Request Email Address";

        }

        public string SendPartsRequestedEmail(decimal ReceiveDetailID, string LocalIP, string UserName)
        {
            if (ReceiveDetailID < 0) { return "Invalid ESN Number"; }
            string subject = "";
            StringBuilder Body = new StringBuilder();
            //MailAttachment[] attachments = new MailAttachment();
            string FriendlyName = "FrendlyName";
            string ESN = "ESN";
            string MasterClient = "Master Client";
            string Carrier = "Carrier";
            string Manufacturer = "Manufacturer";
            string Model = "Model";
            string Colour = "Colour";
            string Part_01 = "";
            string Part_02 = "";
            string Part_03 = "";
            string IFSLocation = "";
            string RequestNote = "";
            //string LocalIP = "LocalIP";
            DateTime SaveTime_Date = DateTime.Now;

            ReceiveDetailManager rdm = new ReceiveDetailManager(UserName);

            using (clsLinqDataContext ctx = rdm.GetDataContext(UserName))
            {
                ReceiveDetail rd = ctx.ReceiveDetails.FirstOrDefault(x => x.ReceiveDetailID == ReceiveDetailID);
                if (rd == null) { return "ESN Not found"; }

                BasicUserUtilities buu = new BasicUserUtilities(UserName);
                FriendlyName = buu.GetThisUserFriendlyName(UserName);
                ESN = rd.ESN;
                MasterClient = rd.ClientLocation.Client.CompanyName;
                Carrier = rd.Carrier;
                Manufacturer = rd.Manufacturer;
                Model = rd.Model;
                Colour = rd.Colour;
                IFSLocation = rd.IFSLocation;

                // Get the "NONE" Value for each of the options.

                foreach (ReceiveDetailItem i in rd.ReceiveDetailItems.Where(x => x.Option.Question.Name == "Requested Part 1" ||
                                                                                x.Option.Question.Name == "Requested Part 2" ||
                                                                                x.Option.Question.Name == "Requested Part 3" ||
                                                                                x.Option.Question.Name == "Request Notes"))
                {
                    if (i.Option.Question.Name == "Requested Part 1")
                    {
                        Part_01 = i.Option.OptionText;
                        //if (Part_01.ToUpper() != "NONE")
                        //{
                        //    Option o = ctx.Options.FirstOrDefault(x => x.QuestionID == i.Option.QuestionID && x.OptionText.ToUpper() == "NONE");
                        //    if (o != null)
                        //    {
                        //        //decimal oID = ctx.Options.FirstOrDefault(x => x.Question.Name == "Requested Part 1" && x.OptionText.ToUpper() == "NONE").OptionID;
                        //        //i.OptionID = 6243;         //o.OptionID;
                        //    }
                        //}
                    }
                    if (i.Option.Question.Name == "Requested Part 2")
                    {
                        Part_02 = i.Option.OptionText;
                        //if (Part_02.ToUpper() != "NONE")
                        //{
                        //    Option o = ctx.Options.FirstOrDefault(x => x.QuestionID == i.Option.QuestionID && x.OptionText.ToUpper() == "NONE");
                        //    if (o != null)
                        //    {
                        //        //decimal oID = ctx.Options.FirstOrDefault(x => x.Question.Name == "Requested Part 1" && x.OptionText.ToUpper() == "NONE").OptionID;
                        //        //i.OptionID = o.OptionID;
                        //        //i.OptionID = 6272;         //o.OptionID;
                        //    }
                        //}
                    }
                    if (i.Option.Question.Name == "Requested Part 3")
                    {
                        Part_03 = i.Option.OptionText;
                        //if (Part_03.ToUpper() != "NONE")
                        //{
                        //    Option o = ctx.Options.FirstOrDefault(x => x.QuestionID == i.Option.QuestionID && x.OptionText.ToUpper() == "NONE");
                        //    if (o != null)
                        //    {
                        //        //decimal oID = ctx.Options.FirstOrDefault(x => x.Question.Name == "Requested Part 1" && x.OptionText.ToUpper() == "NONE").OptionID;
                        //        //i.OptionID = o.OptionID;
                        //        //i.OptionID = 6300;         //o.OptionID;
                        //    }
                        //}
                    }
                    if (i.Option.Question.Name == "Request Notes")
                    {
                        RequestNote = i.Value;
                        i.Value = "";
                    }
                    ctx.SubmitChanges();
                }
                //Part_01 = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Requested Part 1");
                //Part_02 = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Requested Part 2");
                //Part_03 = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Requested Part 3");
                //RequestNote = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Request Notes");


                MasterPartManager pm = new MasterPartManager(UserName);
                if (Part_01.Trim().Length > 0 && Part_01.ToUpper() != "NONE")
                {
                    pm.AddRequestedItem(ReceiveDetailID, rd.IFSLocation, FriendlyName, MasterClient, rd.CarrierID, (decimal)rd.ManufacturerID, rd.ModelID, rd.ColourID, Carrier, Manufacturer, Model, Colour, Part_01, RequestNote);
                }
                if (Part_02.Trim().Length > 0 && Part_02.ToUpper() != "NONE")
                {
                    pm.AddRequestedItem(ReceiveDetailID, rd.IFSLocation, FriendlyName, MasterClient, rd.CarrierID, (decimal)rd.ManufacturerID, rd.ModelID, rd.ColourID, Carrier, Manufacturer, Model, Colour, Part_02, RequestNote);
                }
                if (Part_03.Trim().Length > 0 && Part_03.ToUpper() != "NONE")
                {
                    pm.AddRequestedItem(ReceiveDetailID, rd.IFSLocation, FriendlyName, MasterClient, rd.CarrierID, (decimal)rd.ManufacturerID, rd.ModelID, rd.ColourID, Carrier, Manufacturer, Model, Colour, Part_03, RequestNote);
                }



                subject = "A parts request has been received.";


                Body.Append("<br/><br>");
                Body.Append("--------------------------------------------------------------");
                Body.Append("<br/>");

                Body.Append("Technician:  " + UserName + " (" + FriendlyName + ")");
                Body.Append("<br/><br>");
                Body.Append("ESN:  " + ESN);
                Body.Append("<br/>");
                Body.Append("MasterClient:  " + MasterClient);
                Body.Append("<br/>");
                Body.Append("<br/>");
                Body.Append("Carrier:  " + Carrier);
                Body.Append("<br/>");
                Body.Append("Manufacturer:  " + Manufacturer);
                Body.Append("<br/>");
                Body.Append("Model:  " + Model);
                Body.Append("<br/>");
                Body.Append("Colour:  " + Colour);
                Body.Append("<br/>");
                Body.Append("<br/>");

                Body.Append("Part #1: " + Part_01);
                Body.Append("<br/>");
                Body.Append("Part #2: " + Part_02);
                Body.Append("<br/>");
                Body.Append("Part #3: " + Part_03);
                Body.Append("<br/>");
                Body.Append("<br/>");

                Body.Append("Notes To Parts Department:");
                Body.Append("<br/>");
                Body.Append(RequestNote);
                Body.Append("<br/>");
                Body.Append("--------------------------------------------------------------");
                Body.Append("<br/>");
                Body.Append("<br/><br>");
                Body.Append("This email was generated with");
                Body.Append("<br/>");
                Body.Append("The BRAIN 2.0");
                Body.Append("<br/>");
                Body.Append("https://database@.ca");
                Body.Append("<br/>");
                Body.Append("User Local IP:" + LocalIP);
                Body.Append("<br/>");
                Body.Append("Requested Time/Date:" + SaveTime_Date);
                Body.Append("<br/>");


                //rdm.UpdateESNAttribute_Blank(ctx, ReceiveDetailID, "Request Notes");
                //rdm.UpdateESNAttribute(ctx, ReceiveDetailID, "Requested Part 1", "NONE");
                //rdm.UpdateESNAttribute(ctx, ReceiveDetailID, "Requested Part 2", "NONE");
                //rdm.UpdateESNAttribute(ctx, ReceiveDetailID, "Requested Part 3", "NONE");
            }

            // Log detail to Dashboard

            CompanyDemographics company = new CompanyDemographics(UserName);
            string to = company.PartReqEmailAddress;
            if (to != null && to.Trim().Length > 0)
            {
                return Email(to, Body.ToString(), subject);
            }
            return "";
            //return "Error, Check Company Demographics, missing Parts Request Email Address";

        }


        public string TestSendEmail()
        {
            try
            {

                SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
                var mail = new MailMessage();
                mail.From = new MailAddress("jim.willson@hotmail.com");
                mail.To.Add("jim.willson@hotmail.com");
                mail.Subject = "Test Mail - 1";
                mail.IsBodyHtml = true;
                string htmlBody;
                htmlBody = "Write some HTML code here";
                mail.Body = htmlBody;
                SmtpServer.Port = 25;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("Jim.willson@hotmail.com", "Jamesg01_James");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder(1024);
                //sb.Append("\nTo:" + to);
                //sb.Append("\nbody:" + body);
                //sb.Append("\nsubject:" + subject);
                //sb.Append("\nfromAddress:" + fromAddress);
                //sb.Append("\nfromDisplay:" + fromDisplay);
                //sb.Append("\ncredentialUser:" + credentialUser);
                //sb.Append("\ncredentialPasswordto:" + credentialPassword);
                //sb.Append("\nHosting:" + host);
                sb.Append("\nError:" + ex.ToString());
                return sb.ToString();
                // ErrorLog(sb.ToString(), ex.ToString(), ErrorLogCause.EmailSystem);
            }
            return "Sent";
        }



        public string Email(string to,
                            string body,
                            string subject,
                              params MailAttachment[] attachments)
        {

            //return TestSendEmail();
            //{
            //}

            //string host = System.Configuration.ConfigurationManager.AppSettings["SMTPHost"];
            try
            {
                MailMessage mail = new MailMessage();
                //host  = "smtp.live.com";
                mail.Body = body;
                mail.IsBodyHtml = true;

                string[] To = to.Split(';');
                foreach (string t in To)
                {
                    if (t.Length > 0)
                    {
                        mail.To.Add(new MailAddress(t));
                    }
                }

                //mail.From = new MailAddress(fromAddress, fromDisplay, System.Text.Encoding.UTF8);
                mail.Subject = subject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Priority = MailPriority.Normal;
                foreach (MailAttachment ma in attachments)
                {
                    mail.Attachments.Add(ma.File);
                }
                SmtpClient smtp = new SmtpClient();
                //smtp.Credentials = new System.Net.NetworkCredential(credentialUser, credentialPassword);
                //smtp.Host = host;

                //smtp.Port = 25;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder(1024);
                sb.Append("\nTo:" + to);
                sb.Append("\nbody:" + body);
                sb.Append("\nsubject:" + subject);
                //sb.Append("\nfromAddress:" + fromAddress);
                //sb.Append("\nfromDisplay:" + fromDisplay);
                //sb.Append("\ncredentialUser:" + credentialUser);
                //sb.Append("\ncredentialPasswordto:" + credentialPassword);
                //sb.Append("\nHosting:" + host);
                sb.Append("\nError:" + ex.ToString());
                return sb.ToString();
                // ErrorLog(sb.ToString(), ex.ToString(), ErrorLogCause.EmailSystem);
            }
            return "Email Sent";
        }
    }
}