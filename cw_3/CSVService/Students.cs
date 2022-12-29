using cw_3.Models;
using Microsoft.VisualBasic.FileIO;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace cw_3
{
    public class Students
    {
        
        public string path;
        public static List<Student> student_extent = new List<Student>();
        public Students(string path)
        {
           
            this.path = path;
            readCSV();
        }

        public void readCSV()
        {
            using (TextFieldParser csvParser = new TextFieldParser(this.path))
            {

                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;
                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    string[] fields = csvParser.ReadFields();
                    string id = fields[2];
                    string name = fields[0];
                    string surname = fields[1];
                    string birthdate = fields[3];
                    string field = fields[4];
                    string type = fields[5];
                    string email = fields[6];
                    string mother = fields[8];
                    string father = fields[7];

                    Student test_student = student_extent.Find(i => i.IndexNumber == id);
                    if(test_student == null)
                    {
                        student_extent.Add(new Student
                        {
                            IndexNumber = id,
                            FirstName = name,
                            LastName = surname,
                            BirthDate = birthdate,
                            Field = field,
                            Mode = type,
                            email = email,
                            mother = mother,
                            father = father
                        }

                    );
                    }
                }
            }
           

            
        }
        public List<Student> getExtent()
        {           
            return student_extent;
        }

        public bool AddStudent(Student student)
        {
            if (useRegex(student.IndexNumber))
            {

                string idStudent = student.IndexNumber;
                Student test_student = student_extent.Find(i => i.IndexNumber == idStudent);
                if (test_student == null)
                {
                    if (String.IsNullOrEmpty(student.FirstName) || String.IsNullOrEmpty(student.LastName) || String.IsNullOrEmpty(student.IndexNumber) || String.IsNullOrEmpty(student.BirthDate) || String.IsNullOrEmpty(student.Field) || String.IsNullOrEmpty(student.Mode) || String.IsNullOrEmpty(student.mother) || String.IsNullOrEmpty(student.father))
                    {
                        return false;
                    }
                    else
                    {
                        addAndAppend(this.path, student);
                        return true;
                    }
                }
                else
                {
                    return false;
                }
                return false;
            }
            return false;
            
        }

        public static bool useRegex(String input)
        {
            Regex regex = new Regex("s\\d\\d\\d\\d\\d", RegexOptions.IgnoreCase);
            return regex.IsMatch(input);
        }

        public bool deleteStudent(Student student)
        {
            if (useRegex(student.IndexNumber))
            {

                string idStudent = student.IndexNumber;
                Student test_student = student_extent.Find(i => i.IndexNumber == idStudent);
                if (test_student == null)
                {
                    return false;
                }
                else
                {
                    student_extent.Remove(student);
                    rewriteData();
                    return true;
                }
            } else
            return false;

        }

        public bool modifyStudent(Student student, string index)
        {

            if (useRegex(student.IndexNumber))
            {

                foreach(Student temp_student in student_extent)
                {
                    if(temp_student.IndexNumber == index)
                    {
                        temp_student.FirstName = student.FirstName;
                        temp_student.LastName = student.LastName;
                        temp_student.BirthDate = student.BirthDate;
                        temp_student.Field = student.Field;
                        temp_student.Mode = student.Mode;
                        temp_student.email = student.email;
                        temp_student.mother = student.mother;
                        temp_student.father = student.father;
                    }

                }
             

                rewriteData();
                return true;

            }
            else return false;
        }

        private void addAndAppend(string path, Student student)
        {
            student_extent.Add(student);
            if (File.Exists(path))
            {
                string newStudent = student.FirstName + "," + student.LastName + "," + student.IndexNumber + "," + student.BirthDate + "," + student.Field + "," + student.Mode + "," + student.email + "," + student.mother + "," + student.father + Environment.NewLine;
                File.AppendAllText(path, newStudent);
                
            }
        }





        private void rewriteData()
        {
            StreamWriter myStream = new StreamWriter(path, false);
            foreach (Student student in student_extent)
            {
                myStream.WriteLine(student.FirstName + "," + student.LastName + "," + student.IndexNumber + "," + student.BirthDate + "," + student.Field + "," + student.Mode + "," + student.email + "," + student.mother + "," + student.father);
            }
            myStream.Close();
        }
        }



    }
