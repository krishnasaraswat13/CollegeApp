namespace CollegeApp.Models
{
    public static class CollegeRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>()
        {
                new Student
                {
                    Id=1,
                    StudentName="StudentOne",
                    Email="studentemail1@gmail.com",
                    Address="Hyd, INDIA"
                },
                new Student
                {
                   Id=2,
                   StudentName="StudentTwo",
                   Email="Studentemail2@gmail.com",
                   Address="Banglore,INDIA"
                }
        };
    }
}
