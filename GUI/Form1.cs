using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace GUI
{
    public partial class Form1 : Form
    {
        private readonly StudentService studentService = new StudentService();
        private readonly FacultyService facultyService = new FacultyService();
        private string avatarPath;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                setGridViewStyle(dgvTableSV);
                var listFacultys = facultyService.GetAll();
                var listStudents = studentService.GetAll();
                FillFacultyCombobox(listFacultys);
                BindGrid(listStudents);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillFacultyCombobox(List<Faculty> listFacultys)
        {
            listFacultys.Insert(0, new Faculty { FacultyID = 0, FacultyName = "Chọn khoa" });
            cmbFaculty.DataSource = listFacultys;
            cmbFaculty.DisplayMember = "FacultyName";
            cmbFaculty.ValueMember = "FacultyID";
        }

        private void BindGrid(List<Student> listStudent)
        {
            dgvTableSV.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dgvTableSV.Rows.Add();
                dgvTableSV.Rows[index].Cells[0].Value = item.StudentID;
                dgvTableSV.Rows[index].Cells[1].Value = item.FullName;
                dgvTableSV.Rows[index].Cells[2].Value = item.Faculty?.FacultyName ?? "N/A";
                dgvTableSV.Rows[index].Cells[3].Value = item.AverageScore.ToString();
                dgvTableSV.Rows[index].Cells[4].Value = item.Major?.Name ?? "Chưa có chuyên ngành";

                // Display avatar from byte array if available
                ShowAvatar(item.Avatar);
            }
        }

        private void ShowAvatar(byte[] avatarBytes)
        {
            if (avatarBytes == null || avatarBytes.Length == 0)
            {
                pictureAvatar.Image = null;  // Clear avatar if no data
            }
            else
            {
                try
                {
                    using (var ms = new MemoryStream(avatarBytes))
                    {
                        pictureAvatar.Image = Image.FromStream(ms);  // Display avatar image
                    }
                    pictureAvatar.Refresh();  // Refresh PictureBox to show new image
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể hiển thị ảnh: " + ex.Message);  // Show error if displaying image fails
                }
            }
        }


        private void setGridViewStyle(DataGridView dgview)
        {
            dgview.BorderStyle = BorderStyle.None;
            dgview.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgview.BackgroundColor = Color.White;
            dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var listStudents = checkBox1.Checked ? studentService.GetAllHasNoMajor() : studentService.GetAll();
            BindGrid(listStudents);
        }

        private void btnThemSua_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ form
                string studentId = txtMSSV.Text;
                string fullName = txtHoten.Text;
                if (!double.TryParse(txtDTB.Text, out double averageScore))
                {
                    MessageBox.Show("Điểm trung bình không hợp lệ!");
                    return;
                }

                int? facultyId = cmbFaculty.SelectedValue as int?;

                // Xử lý avatar nếu người dùng đã chọn hình ảnh
                byte[] avatarBytes = null;
                if (!string.IsNullOrEmpty(avatarPath) && File.Exists(avatarPath))
                {
                    avatarBytes = File.ReadAllBytes(avatarPath);
                }

                // Kiểm tra sinh viên đã tồn tại chưa
                var student = studentService.FindById(studentId);
                if (student == null)
                {
                    // Thêm sinh viên mới
                    student = new Student
                    {
                        StudentID = studentId,
                        FullName = fullName,
                        AverageScore = averageScore,
                        FacultyID = facultyId,
                        Avatar = avatarBytes
                    };
                    studentService.InsertUpdate(student);
                    MessageBox.Show("Thêm sinh viên thành công!");
                }
                else
                {
                    // Cập nhật sinh viên
                    student.FullName = fullName;
                    student.AverageScore = averageScore;
                    student.FacultyID = facultyId;
                    student.Avatar = avatarBytes ?? student.Avatar; // Giữ avatar cũ nếu không thay đổi
                    studentService.InsertUpdate(student);
                    MessageBox.Show("Cập nhật sinh viên thành công!");
                }

                // Tải lại danh sách sinh viên
                var listStudents = studentService.GetAll();
                BindGrid(listStudents);
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        MessageBox.Show($"Thuộc tính: {validationError.PropertyName} Lỗi: {validationError.ErrorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTableSV.SelectedRows.Count > 0)
                {
                    string studentId = dgvTableSV.SelectedRows[0].Cells[0].Value.ToString();
                    // Gọi phương thức Delete với studentId
                    studentService.Delete(studentId);
                    MessageBox.Show("Xóa sinh viên thành công!");

                    // Tải lại danh sách sinh viên
                    var listStudents = studentService.GetAll();
                    BindGrid(listStudents);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn sinh viên để xóa!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btnAddPicture_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    avatarPath = ofd.FileName; // Save selected image path
                    pictureAvatar.Image = Image.FromFile(avatarPath); // Display image
                    pictureAvatar.Refresh();
                }
            }
        }

        private void quảnLýKhoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void dgvTableSV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy hàng được chọn
                DataGridViewRow row = dgvTableSV.Rows[e.RowIndex];

                // Đổ dữ liệu vào các TextBox
                txtMSSV.Text = row.Cells[0].Value.ToString();
                txtHoten.Text = row.Cells[1].Value.ToString();
                txtDTB.Text = row.Cells[3].Value.ToString(); // Điểm trung bình

                // Xử lý combobox khoa
                cmbFaculty.SelectedValue = row.Cells[2].Value; // Chọn khoa tương ứng

                // Xử lý hình ảnh (nếu có avatar)
                byte[] avatarBytes = row.Cells[4].Value as byte[];  // Cột 4 là Avatar
                ShowAvatar(avatarBytes);
            }
        }

        private void btn_LoadDuLieu_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy danh sách khoa và sinh viên từ cơ sở dữ liệu
                var listFacultys = facultyService.GetAll();
                var listStudents = studentService.GetAll();

                // Cập nhật dữ liệu vào combobox Khoa
                FillFacultyCombobox(listFacultys);

                // Cập nhật dữ liệu vào DataGridView
                BindGrid(listStudents);

                MessageBox.Show("Dữ liệu đã được tải lại thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}