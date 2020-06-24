using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class SurveyData
    {
        public void AddSurvey(SurveyDetails details)
        {
            string conString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("spAddSurvey", con);
                command.CommandType = CommandType.StoredProcedure;

               // SqlParameter PID = new SqlParameter();
               // PID.ParameterName = "@ID";
               // PID.Direction = ParameterDirection.ReturnValue;
               //// PID.Value = details.Name;
               // command.Parameters.Add(PID);

                SqlParameter PName = new SqlParameter();
                PName.ParameterName = "@Name";
                PName.Value = details.Name;
                command.Parameters.Add(PName);

                SqlParameter PAge = new SqlParameter();
                PAge.ParameterName = "@Age";
                PAge.Value = details.Age;
                command.Parameters.Add(PAge);

                SqlParameter PGender = new SqlParameter();
                PGender.ParameterName = "@Gender";
                PGender.Value = details.Gender;
                command.Parameters.Add(PGender);

                SqlParameter PEmail = new SqlParameter();
                PEmail.ParameterName = "@Email";
                PEmail.Value = details.Email;
                command.Parameters.Add(PEmail);

                SqlParameter PCity = new SqlParameter();
                PCity.ParameterName = "@City";
                PCity.Value = details.City;
                command.Parameters.Add(PCity);

                SqlParameter PResume = new SqlParameter();
                PResume.ParameterName = "@Resume";
                PResume.Value = details.Resume;
                command.Parameters.Add(PResume);

                SqlParameter PEducation = new SqlParameter();
                PEducation.ParameterName = "@Education";
                PEducation.Value = details.Education;
                command.Parameters.Add(PEducation);

                con.Open();
                command.ExecuteNonQuery();
                //con.Close();
                //Console.WriteLine(PID.Value);
            }
        }

        public IEnumerable<SurveyDetails> Details
        {

            get
            {

                string connectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;

                List<SurveyDetails> details = new List<SurveyDetails>();

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {

                    SqlCommand cmd = new SqlCommand("spGetDetails", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        SurveyDetails surveyDetails = new SurveyDetails();
                        surveyDetails.ID = Convert.ToInt16(rdr["ID"]);
                        surveyDetails.Name = rdr["Name"].ToString();
                        surveyDetails.Age = Convert.ToInt16(rdr["Age"]);
                        surveyDetails.Gender = rdr["Gender"].ToString();
                        surveyDetails.Email = rdr["Email"].ToString();
                        surveyDetails.City = rdr["City"].ToString();
                        surveyDetails.Resume = rdr["Resume"].ToString();
                        surveyDetails.Education = rdr["Education"].ToString();

                        details.Add(surveyDetails);
                    }
                }
                return details;

            }
        }

    }
}
