using System;
using System.Windows.Forms;

namespace FitnessClubApp
{
    public partial class EditPersonalTrainingForm : Form
    {
        private PersonalTraining _training;
        private AppDbContext _context;

        private TextBox txtTitle;
        private NumericUpDown nudDuration;
        private TextBox txtClientName;
        private TextBox txtEquipment;
        private NumericUpDown nudIntensity;
        private Button btnSave;
        private Button btnCancel;

        public EditPersonalTrainingForm(PersonalTraining training, AppDbContext context)
        {
            _training = training;
            _context = context;
            InitializeComponent();
            LoadTrainingData();
        }

        private void InitializeComponent()
        {
            this.Text = "Редактирование персональной тренировки";
            this.Size = new System.Drawing.Size(400, 350);
            this.StartPosition = FormStartPosition.CenterParent;

            var lblTitle = new Label() { Text = "Название:", Location = new System.Drawing.Point(20, 20), Size = new System.Drawing.Size(100, 25) };
            txtTitle = new TextBox() { Location = new System.Drawing.Point(130, 20), Size = new System.Drawing.Size(220, 25) };

            var lblDuration = new Label() { Text = "Длительность (мин):", Location = new System.Drawing.Point(20, 60), Size = new System.Drawing.Size(120, 25) };
            nudDuration = new NumericUpDown() { Location = new System.Drawing.Point(150, 60), Size = new System.Drawing.Size(100, 25), Minimum = 1, Maximum = 180 };

            var lblClient = new Label() { Text = "Клиент:", Location = new System.Drawing.Point(20, 100), Size = new System.Drawing.Size(100, 25) };
            txtClientName = new TextBox() { Location = new System.Drawing.Point(130, 100), Size = new System.Drawing.Size(220, 25) };

            var lblEquipment = new Label() { Text = "Оборудование:", Location = new System.Drawing.Point(20, 140), Size = new System.Drawing.Size(100, 25) };
            txtEquipment = new TextBox() { Location = new System.Drawing.Point(130, 140), Size = new System.Drawing.Size(220, 25) };

            var lblIntensity = new Label() { Text = "Интенсивность (1-10):", Location = new System.Drawing.Point(20, 180), Size = new System.Drawing.Size(130, 25) };
            nudIntensity = new NumericUpDown() { Location = new System.Drawing.Point(160, 180), Size = new System.Drawing.Size(60, 25), Minimum = 1, Maximum = 10 };

            btnSave = new Button() { Text = "Сохранить", Location = new System.Drawing.Point(100, 240), Size = new System.Drawing.Size(100, 35) };
            btnSave.Click += btnSave_Click;

            btnCancel = new Button() { Text = "Отмена", Location = new System.Drawing.Point(220, 240), Size = new System.Drawing.Size(100, 35) };
            btnCancel.Click += (s, e) => this.Close();

            Controls.AddRange(new Control[] { lblTitle, txtTitle, lblDuration, nudDuration, lblClient, txtClientName, lblEquipment, txtEquipment, lblIntensity, nudIntensity, btnSave, btnCancel });
        }

        private void LoadTrainingData()
        {
            txtTitle.Text = _training.Title;
            nudDuration.Value = _training.Duration;
            txtClientName.Text = _training.ClientName;
            txtEquipment.Text = _training.Equipment;
            nudIntensity.Value = _training.IntensityLevel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _training.Title = txtTitle.Text;
                _training.Duration = (int)nudDuration.Value;
                _training.ClientName = txtClientName.Text;
                _training.Equipment = txtEquipment.Text;
                _training.IntensityLevel = (int)nudIntensity.Value;

                var dbTraining = _context.PersonalTrainings.Find(_training.Id);
                if (dbTraining != null)
                {
                    dbTraining.Title = _training.Title;
                    dbTraining.Duration = _training.Duration;
                    dbTraining.ClientName = _training.ClientName;
                    dbTraining.Equipment = _training.Equipment;
                    dbTraining.IntensityLevel = _training.IntensityLevel;
                    _context.SaveChanges();
                }

                MessageBox.Show("Изменения сохранены!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}