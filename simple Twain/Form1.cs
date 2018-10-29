using System;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using Saraff.Twain;

namespace simple_Twain
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                twain.CloseDSM();
                twain.CloseDataSource();
                if (twain.SelectSource())
                {
                    twain.Language = Saraff.Twain.TwLanguage.ENGLISH;
                    twain.Acquire();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            

        }

        private void twain_AcquireCompleted(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < twain.ImageCount; i++)
                {
                    string filePath = $".\\Photo_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.tif";
                    ImageCodecInfo image = ImageCodecInfo.GetImageEncoders().FirstOrDefault(x => x.MimeType == "image/tiff");
                    Encoder myEncoder = Encoder.Compression;
                    EncoderParameters encoderParameters = new EncoderParameters(1);
                    EncoderParameter encoderParameter = new EncoderParameter(myEncoder, (long)EncoderValue.CompressionCCITT4);
                    encoderParameters.Param[0] = encoderParameter;
                    twain.GetImage(i).Save(filePath, image, encoderParameters);
                    MessageBox.Show(this, "Complete file in your project", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
          
        }
    }
}
