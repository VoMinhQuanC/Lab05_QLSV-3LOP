using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form2 : Form
    {
        private readonly StudentService studentService = new StudentService();
        private readonly FacultyService facultyService = new FacultyService();
        private readonly MajorService majorService = new MajorService();

        public Form2()
        {
            InitializeComponent();
        }
        private void FillFalcultyCombobox(List<Faculty> listFacultys)
        {
            this.cmbFaculty.DataSource = listFacultys;
            this.cmbFaculty.DisplayMember = "FacultyName";
            this.cmbFaculty.ValueMember = "FacultyID";
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                // Thêm các cột vào DataGridView nếu chưa có
                if (dgv_Student.Columns.Count == 0)
                {
                    DataGridViewCheckBoxColumn chkColumn = new DataGridViewCheckBoxColumn();
                    chkColumn.HeaderText = "Chọn";
                    chkColumn.Name = "chkSelect";
                    dgv_Student.Columns.Add(chkColumn);

                    dgv_Student.Columns.Add("StudentID", "Mã sinh viên");
                    dgv_Student.Columns.Add("FullName", "Họ tên");
                    dgv_Student.Columns.Add("FacultyName", "Khoa");
                    dgv_Student.Columns.Add("AverageScore", "Điểm trung bình");
                }

                var listFacultys = facultyService.GetAll();
                FillFalcultyCombobox(listFacultys);
                var listStudents = studentService.GetAll();
                BindGrid(listStudents);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BindGrid(List<Student> listStudent)
        {
            dgv_Student.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dgv_Student.Rows.Add();
                dgv_Student.Rows[index].Cells["StudentID"].Value = item.StudentID;
                dgv_Student.Rows[index].Cells["FullName"].Value = item.FullName;
                dgv_Student.Rows[index].Cells["FacultyName"].Value = item.Faculty.FacultyName;
                dgv_Student.Rows[index].Cells["AverageScore"].Value = item.AverageScore.ToString();
            }
        }
        private void FillMajorCombobox(List<Major> listMajors)
        {
            this.cmbMajor.DataSource = listMajors;
            this.cmbMajor.DisplayMember = "Name";  // Hiển thị tên chuyên ngành
            this.cmbMajor.ValueMember = "MajorID"; // Sử dụng MajorID để gán giá trị
        }

        private void cmbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            Faculty selectedFaculty = cmbFaculty.SelectedItem as Faculty;
            if (selectedFaculty != null)
            {
                // Lọc danh sách chuyên ngành dựa trên khoa đã chọn
                var listMajor = majorService.GetAllByFaculty(selectedFaculty.FacultyID);
                if (listMajor == null || listMajor.Count == 0)
                {
                    MessageBox.Show("Không có chuyên ngành nào cho khoa đã chọn.", "Thông báo");
                    cmbMajor.DataSource = null;
                    return;
                }

                FillMajorCombobox(listMajor);

                // Lấy danh sách sinh viên chưa có chuyên ngành từ khoa được chọn
                var listStudents = studentService.GetAllHasNoMajor(selectedFaculty.FacultyID);
                BindGrid(listStudents);
            }
        }

        private void cmbMajor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_DangKi_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgv_Student.Rows)
            {
                // Kiểm tra nếu CheckBox được chọn
                if (Convert.ToBoolean(row.Cells["chkSelect"].Value) == true)
                {
                    string studentID = row.Cells["StudentID"].Value.ToString();
                    Student student = studentService.FindById(studentID);

                    // Gán chuyên ngành đã chọn
                    student.MajorID = Convert.ToInt32(cmbMajor.SelectedValue);
                    studentService.InsertUpdate(student);
                }
            }
            MessageBox.Show("Đăng ký chuyên ngành thành công!");
        }
    }
}