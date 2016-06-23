using COMP2007_S2016_MIDTERM_200306422.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COMP2007_S2016_MIDTERM_200306422
{
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetTodoList();
            }
        }

        protected void GetTodoList()
        {
            // populate teh form with existing data from the database
            int TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

            // connect to the  DB
            using (TodoConnection db = new TodoConnection())
            {
               
                Todo updatedList = (from Todo in db.Todos
                                          where Todo.TodoID == TodoID
                                    select Todo).FirstOrDefault();

                
                if (updatedList != null)
                {
                    TodoNameTextBox.Text = updatedList.TodoName;
                    NotesTextBox.Text = updatedList.TodoNotes;
                    Completed.Checked = Convert.ToBoolean(updatedList.Completed);
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // Redirect back to TodoList page
            Response.Redirect("~/TodoList.aspx");

        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Use EF to connect to the server
            using (TodoConnection db = new TodoConnection())
            {
                //create objects
                // save a new record
                Todo newTodo = new Todo();

                int TodoID = 0;

                if (Request.QueryString.Count > 0) // our URL has a TodoID in it
                {
                    // get the id from the URL
                    TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

                    
                    newTodo = (from Todo in db.Todos
                                  where Todo.TodoID == TodoID
                               select Todo).FirstOrDefault();
                }

                //add new records
                newTodo.TodoName = TodoNameTextBox.Text;
                newTodo.TodoNotes = NotesTextBox.Text;
                Completed.Checked = Convert.ToBoolean(newTodo.Completed);
               
                // use LINQ to ADO.NET to add / insert new Todo into the database

                if (TodoID == 0)
                {
                    db.Todos.Add(newTodo);
                }


                // save our changes - also updates and inserts
                db.SaveChanges();

                // Redirect back to the updated List page
                Response.Redirect("~/TodoList.aspx");
            }
        }

       
    }
    }
