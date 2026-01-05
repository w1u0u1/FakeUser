using Bogus;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using System;
using System.Text;
using System.Windows.Forms;

namespace FakeUser
{
    public partial class Form1 : XtraForm
    {
        public class User
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Gender { get; set; }
            public DateTime Birthday { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

            public string Country { get; set; }
            public string Region { get; set; }
            public string City { get; set; }
            public string Address { get; set; }
            public string ZipCode { get; set; }

            public string CreditCardNumber { get; set; }
            public string CVV { get; set; }
            public string ExpiryDate { get; set; }

            public string Company { get; set; }
            public string Department { get; set; }
            public string JobTitle { get; set; }
        }

        public Form1()
        {
            InitializeComponent();

            repositoryItemComboBox1.Items.AddRange(new string[] { "United States", "France", "Germany", "Spain", "Japan", "China" });
            beiCountry.EditValue = repositoryItemComboBox1.Items[0];

            repositoryItemComboBox2.Items.AddRange(new string[] { "Male", "Female", "Other" });
            beiGender.EditValue = repositoryItemComboBox2.Items[0];

            repositoryItemComboBox3.Items.AddRange(new int[] { 10, 20, 30, 50, 100 });
            beiCount.EditValue = repositoryItemComboBox3.Items[0];
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.HitTest == GridHitTest.EmptyRow || e.HitInfo.HitTest == GridHitTest.RowCell)
            {
                e.Allow = false;
                popupMenu1.ShowPopup(gridControl1.PointToScreen(e.Point));
            }
        }

        private void repositoryItemComboBox1_EditValueChanged(object sender, EventArgs e)
        {
            var editor = sender as ComboBoxEdit;

            if (editor.EditValue == null || string.IsNullOrEmpty(editor.EditValue.ToString()))
                editor.EditValue = "United States";
        }

        private void repositoryItemComboBox3_EditValueChanged(object sender, EventArgs e)
        {
            var editor = sender as ComboBoxEdit;

            if (editor.EditValue == null || string.IsNullOrEmpty(editor.EditValue.ToString()))
                editor.EditValue = 10;
        }

        private void repositoryItemComboBox3_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            // Allow only digits, backspace, and delete
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete)
                e.Handled = true;
        }

        private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int count = Convert.ToInt32(beiCount.EditValue);
                string selectedCountry = beiCountry.EditValue.ToString();
                string selectedGender = beiGender.EditValue.ToString();

                string locale = LocaleHelper.GetLocale(selectedCountry);

                var genderRule = selectedGender == "Male" ? new[] { "Male" } :
                                 selectedGender == "Female" ? new[] { "Female" } : new[] { "Male", "Female", "Other" };

                var faker = new Faker<User>(locale)
                    .RuleFor(u => u.Gender, f => f.PickRandom(genderRule))
                    .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(u.Gender == "Male" ? Bogus.DataSets.Name.Gender.Male : Bogus.DataSets.Name.Gender.Female))
                    .RuleFor(u => u.LastName, (f, u) => f.Name.LastName(u.Gender == "Male" ? Bogus.DataSets.Name.Gender.Male : Bogus.DataSets.Name.Gender.Female))

                    .RuleFor(u => u.Birthday, f => f.Date.Past(60, DateTime.Now.AddYears(-18))) // 18-60 years old
                    .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                    .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())

                    .RuleFor(u => u.Country, f => selectedCountry)
                    .RuleFor(u => u.Region, f => f.Address.State())
                    .RuleFor(u => u.City, f => f.Address.City())
                    .RuleFor(u => u.Address, f => f.Address.StreetAddress())
                    .RuleFor(u => u.ZipCode, f => f.Address.ZipCode())

                    .RuleFor(u => u.CreditCardNumber, f => f.Finance.CreditCardNumber())
                    .RuleFor(u => u.CVV, f => f.Finance.CreditCardCvv())
                    .RuleFor(u => u.ExpiryDate, f => f.Date.Future(5).ToString("MM/yy"))

                    .RuleFor(u => u.Company, f => f.Company.CompanyName())
                    .RuleFor(u => u.Department, f => f.Commerce.Department())
                    .RuleFor(u => u.JobTitle, f => f.Name.JobTitle());

                var users = faker.Generate(count);

                gridControl1.DataSource = users;
                gridView1.BestFitColumns();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void bbiExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.RowCount == 0)
                return;

            var xsfd = new XtraSaveFileDialog();
            xsfd.Filter = "Csv Files(*.csv)|*.csv|Xlsx Files(*.xlsx)|*.xlsx";
            xsfd.FilterIndex = 2;
            if (xsfd.ShowDialog() == DialogResult.OK)
            {
                switch (xsfd.FilterIndex)
                {
                    case 1:
                        {
                            var options = new CsvExportOptionsEx();
                            options.Separator = ",";
                            options.QuoteStringsWithSeparators = true;
                            options.TextExportMode = TextExportMode.Text;
                            options.Encoding = Encoding.UTF8;
                            options.WritePreamble = true;
                            gridView1.ExportToCsv(xsfd.FileName, options);
                        }
                        break;
                    case 2:
                        gridView1.ExportToXlsx(xsfd.FileName);
                        break;
                }
            }
        }
    }
}