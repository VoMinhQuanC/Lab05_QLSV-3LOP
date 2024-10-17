using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MajorService
    {
        public List<Major> GetAllByFaculty(int facultyId)
        {
            using (var context = new StudentModel())
            {
                return context.Major.Where(m => m.FacultyID == facultyId).ToList();
            }
        }

    }

}