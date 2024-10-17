using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace BLL
{
    public class StudentService
    {
        public List<Student> GetAll()
        {
            try
            {
                using (StudentModel context = new StudentModel())
                {
                    return context.Student.ToList();
                }
            }
            catch (Exception ex)
            {
                // Ghi lại thông tin lỗi
                Console.WriteLine("Lỗi khi lấy danh sách sinh viên: " + ex.Message);
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
                return new List<Student>(); // Trả về danh sách rỗng nếu có lỗi
            }
        }

        public List<Student> GetAllHasNoMajor()
        {
            try
            {
                using (StudentModel context = new StudentModel())
                {
                    return context.Student.Where(p => p.MajorID == null).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy sinh viên không có chuyên ngành: " + ex.Message);
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
                return new List<Student>();
            }
        }

        public List<Student> GetAllHasNoMajor(int facultyID)
        {
            try
            {
                using (StudentModel context = new StudentModel())
                {
                    return context.Student.Where(p => p.MajorID == null && p.FacultyID == facultyID).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy sinh viên không có chuyên ngành theo khoa: " + ex.Message);
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
                return new List<Student>();
            }
        }

        public Student FindById(string studentId)
        {
            try
            {
                using (StudentModel context = new StudentModel())
                {
                    return context.Student.FirstOrDefault(p => p.StudentID == studentId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tìm sinh viên theo ID: " + ex.Message);
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
                return null; // Trả về null nếu có lỗi
            }
        }

        public void InsertUpdate(Student s)
        {
            try
            {
                using (StudentModel context = new StudentModel())
                {
                    context.Student.AddOrUpdate(s);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm hoặc cập nhật sinh viên: " + ex.Message);
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
            }
        }

        public void Delete(string studentId)
        {
            try
            {
                using (var context = new StudentModel())
                {
                    var student = context.Student.FirstOrDefault(s => s.StudentID == studentId);
                    if (student != null)
                    {
                        context.Student.Remove(student);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa sinh viên: " + ex.Message);
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
            }
        }
    }
}
