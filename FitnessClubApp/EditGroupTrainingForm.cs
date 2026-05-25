using System;
using System.Windows.Forms;

namespace FitnessClubApp
{
    public partial class EditGroupTrainingForm : Form
    {
        private GroupTraining _training;
        private AppDbContext _context;

        private TextBox txtTitle;
        private NumericUpDown nudDuration;
        private TextBox txtTrainerName;
        private NumericUpDown nudRoomNumber;
        private NumericUpDown nudMaxParticipants;
        private Button btnSave;
        private Button btnCancel;

        public EditGroupTrainingForm(GroupTraining training, AppDbContext context)
        {
            _training = training;
            _context = context;
            InitializeComponent();
            LoadTrainingData();
        }

        private void InitializeComponent()
        {
            this.Text = "Редактирование групповой тренировки";
            this.Size = new System.Drawing.Size(400, 350);
            this.StartPosition = FormStartPosition.CenterParent;

            var lblTitle = new Label() { Text = "Название:", Location = new System.Drawing.Point(20, 20), Size = new System.Drawing.Size(100, 25) };
            txtTitle = new TextBox() { Location = new System.Drawing.Point(130, 20), Size = new System.Drawing.Size(220, 25) };

            var lblDuration = new Label() { Text = "Длительность (мин):", Location = new System.Drawing.Point(20, 60), Size = new System.Drawing.Size(120, 25) };
            nudDuration = new NumericUpDown() { Location = new System.Drawing.Point(150, 60), Size = new System.Drawing.Size(100, 25), Minimum = 1, Maximum = 180 };

            var lblTrainer = new Label() { Text = "Тренер:", Location = new System.Drawing.Point(20, 100), Size = new System.Drawing.Size(100, 25) };
            txtTrainerName = new TextBox() { Location = new System.Drawing.Point(130, 100), Size = new System.Drawing.Size(220, 25) };

            var lblRoom = new Label() { Text = "Номер зала:", Location = new System.Drawing.Point(20, 140), Size = new System.Drawing.Size(100, 25) };
            nudRoomNumber = new NumericUpDown() { Location = new System.Drawing.Point(130, 140), Size = new System.Drawing.Size(100, 25), Minimum = 1, Maximum = 50 };

            var lblMaxPart = new Label() { Text = "Макс. участников:", Location = new System.Drawing.Point(20, 180), Size = new System.Drawing.Size(120, 25) };
            nudMaxParticipants = new NumericUpDown() { Location = new System.Drawing.Point(150, 180), Size = new System.Drawing.Size(100, 25), Minimum = 1, Maximum = 100 };

            btnSave = new Button() { Text = "Сохранить", Location = new System.Drawing.Point(100, 240), Size = new System.Drawing.Size(100, 35) };
            btnSave.Click += btnSave_Click;

            btnCancel = new Button() { Text = "Отмена", Location = new System.Drawing.Point(220, 240), Size = new System.Drawing.Size(100, 35) };
            btnCancel.Click += (s, e) => this.Close();

            Controls.AddRange(new Control[] { lblTitle, txtTitle, lblDuration, nudDuration, lblTrainer, txtTrainerName, lblRoom, nudRoomNumber, lblMaxPart, nudMaxParticipants, btnSave, btnCancel });
        }

        private void LoadTrainingData()
        {
            txtTitle.Text = _training.Title;
            nudDuration.Value = _training.Duration;
            txtTrainerName.Text = _training.TrainerName;
            nudRoomNumber.Value = _training.RoomNumber;
            nudMaxParticipants.Value = _training.MaxParticipants;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _training.Title = txtTitle.Text;
                _training.Duration = (int)nudDuration.Value;
                _training.TrainerName = txtTrainerName.Text;
                _training.RoomNumber = (int)nudRoomNumber.Value;
                _training.MaxParticipants = (int)nudMaxParticipants.Value;

                var dbTraining = _context.GroupTrainings.Find(_training.Id);
                if (dbTraining != null)
                {
                    dbTraining.Title = _training.Title;
                    dbTraining.Duration = _training.Duration;
                    dbTraining.TrainerName = _training.TrainerName;
                    dbTraining.RoomNumber = _training.RoomNumber;
                    dbTraining.MaxParticipants = _training.MaxParticipants;
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