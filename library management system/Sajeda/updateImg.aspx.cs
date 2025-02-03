//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace LibraryProject
//{
//    public partial class updateImg : System.Web.UI.Page
//    {
//            private string imageFolderPath;
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            imageFolderPath = Server.MapPath("~/img/");

//        }


//        protected void edit_Click(object sender, EventArgs e)
//        {
//            //string users = Server.MapPath("users.txt");
//            //if (File.Exists(users))
//            //{
//            //    string[] readData = File.ReadAllLines(users);

//            //    for (int i = 0; i < readData.Length; i++)
//            //    {
//            //        string[] studentData = readData[i].Split(',');
//            //        if (studentData[7] == "true")
//            //        {
//            //            readData[i] = $"{studentData[0]},{name.Text},{email.Text},{phone.Text},{studentData[6]},true";
//            //            File.WriteAllLines(users, readData);
//            //            //saved.Text = "saved!";
//            //        }
//            //    }
//            //}
//        }


//        protected void resetPassword_Click(object sender, EventArgs e)
//        {
//            Response.Redirect("resetPassword.aspx");
//        }

//        protected void showRooms_Click(object sender, EventArgs e)
//        {
//            Response.Redirect("rooms.aspx");
//        }

//        protected void showBooks_Click(object sender, EventArgs e)
//        {
//            Response.Redirect("Books.aspx");
//        }

//        protected void editimg_Click(object sender, EventArgs e)
//        {
//            string imgPath = "img/img1.png";
//            if (fileUpload.HasFile)
//            {
//                string fileName = Path.GetFileName(fileUpload.FileName);
//                string savePath = Path.Combine(imageFolderPath, fileName);
//                fileUpload.SaveAs(savePath);
//                imgPath = "img/" + fileName;
//            }



//        }

//        protected void update_Click(object sender, EventArgs e)
//        {

//        }

//        protected void cancel_Click(object sender, EventArgs e)
//        {
//            Response.Redirect("Profile.aspx");
//        }
//    }
//}

using System;
using System.Data.SqlClient;
using System.IO;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Group5
{
    public partial class updateImg : System.Web.UI.Page
    {
        private string imageFolderPath;
        private string imgTxtFile;

        protected void Page_Load(object sender, EventArgs e)
        {
            imageFolderPath = Server.MapPath("~/img/");
            imgTxtFile = Server.MapPath("~/img/img.txt");

            if (!IsPostBack)
            {
                LoadProfileImage();
            }
        }

        private void LoadProfileImage()
        {
            if (File.Exists(imgTxtFile))
            {
                string[] lines = File.ReadAllLines(imgTxtFile);
                if (lines.Length > 0)
                {
                    imgProfile.ImageUrl = lines[0]; // Load the first saved image
                    Session["UserImagePath"] = lines[0];
                }
                else
                {
                    imgProfile.ImageUrl = "/img/default.png"; // Default image if file is empty
                }
            }
            else
            {
                imgProfile.ImageUrl = "/img/default.png"; // Default image if file doesn't exist
            }
        }

        protected void Editimg_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                try
                {
                    // Ensure the image folder exists
                    if (!Directory.Exists(imageFolderPath))
                    {
                        Directory.CreateDirectory(imageFolderPath);
                    }

                    // Generate a unique filename
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(fileUpload.FileName);
                    string savePath = Path.Combine(imageFolderPath, fileName);

                    // Save the uploaded file
                    fileUpload.SaveAs(savePath);

                    // Store relative path
                    string imagePath = "/img/" + fileName;

                    // Ensure img.txt file exists
                    if (!File.Exists(imgTxtFile))
                    {
                        File.Create(imgTxtFile).Close(); // Create and close the file
                    }

                    // Save image path to img.txt (overwrite previous image path)
                    File.WriteAllText(imgTxtFile, imagePath);

                    // Store in session to reflect update immediately
                    Session["UserImagePath"] = imagePath;

                    // Update the displayed image
                    imgProfile.ImageUrl = imagePath;

                    // Show success message
                    Editmessage.Text = "Image uploaded successfully!";
                    Editmessage.ForeColor = System.Drawing.Color.Green;
                    Editmessage.Visible = true;
                }
                catch (Exception ex)
                {
                    Editmessage.Text = "Error: " + ex.Message;
                    Editmessage.ForeColor = System.Drawing.Color.Red;
                    Editmessage.Visible = true;
                }
            }
            else
            {
                Editmessage.Text = "Please select an image.";
                Editmessage.ForeColor = System.Drawing.Color.Red;
                Editmessage.Visible = true;
            }
            Response.Redirect("editProfile.aspx");
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
        }
    





//if (File.Exists(imgfile))
//{
//    //string[] userImg = File.ReadAllLines(imgfile);

//    using (StreamWriter writeBook2 = new StreamWriter(imgfile,false))
//    {
//        writeBook2.WriteLine($"{fileUpload.Text}");
//    }


//if (fileUpload.HasFile)
//{
//    string fileName = Path.GetFileName(fileUpload.FileName);
//    string savePath = Path.Combine(imageFolderPath, fileName);
//    fileUpload.SaveAs(savePath);
//    imgPath = "img/" + fileName;
//    string newimg = $"{imgPath}";
//    File.AppendAllLines(imgfile, new[] { newimg });

//}







//string usersFile = Server.MapPath("users.txt");
//if (File.Exists(usersFile))
//{
//    string[] readData = File.ReadAllLines(usersFile);
//    for (int i = 0; i < readData.Length; i++)
//    {
//        string[] studentData = readData[i].Split(',');
//        if (studentData[7] == "true")
//        {
//            // Add the image path as the last index
//            Array.Resize(ref studentData, studentData.Length + 1);
//            studentData[8] = imgPath;
//            readData[i] = string.Join(",", studentData);
//            File.WriteAllLines(usersFile, readData);
//            break; // Stop after updating the correct record
//        }
//    }
//}


protected void resetPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("resetPassword.aspx");
        }

        protected void showRooms_Click(object sender, EventArgs e)
        {
            Response.Redirect("rooms.aspx");
        }

        protected void showBooks_Click(object sender, EventArgs e)
        {
            Response.Redirect("Books.aspx");
        }

        protected void edit_Click(object sender, EventArgs e)
        {
            // Edit user details
        }

        protected void update_Click(object sender, EventArgs e)
        {
            // Update user data
        }

        //protected void cancel_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("Profile.aspx");
        //}
    } 
}
