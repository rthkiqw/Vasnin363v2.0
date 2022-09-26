using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study
{
    public class PageControl
    {
        private static AddEmployeePage addEmployeePage;
        public static AddEmployeePage AddEmployeePage
        {
            get
            {
                if (addEmployeePage == null)
                    addEmployeePage = new AddEmployeePage();
                return addEmployeePage;
            }
        }
        private static CreateAccPage createacc;
        public static CreateAccPage CreateAccPage
        {
            get
            {
                if (createacc == null)
                    createacc = new CreateAccPage();
                return createacc;
            }
        }
        private static Authorization authorization;
        public static Authorization Authorization
        {
            get
            {
                if(authorization == null)
                    authorization = new Authorization();
                return authorization;
            }
        }

        private static MainPage mainPage;
        public static MainPage main_page
        {
            get
            {
                if (mainPage == null)
                    mainPage = new MainPage();
                return mainPage;
            }
        }
        private static CreateStudentPage student_page;
        public static CreateStudentPage createStudent
        {
            get
            {
                if (student_page == null)
                    student_page = new CreateStudentPage();
                return student_page;
            }
        }
        private static CreateGroupPage group_page;
        public static CreateGroupPage createGroup
        {
            get
            {
                if (group_page == null)
                    group_page = new CreateGroupPage();
                return group_page;
            }
        }
        private static SpecialityFrame spec_page;
        public static SpecialityFrame createSpec
        {
            get
            {
                if (spec_page == null)
                    spec_page = new SpecialityFrame();
                return spec_page;
            }
        }
    }
}
