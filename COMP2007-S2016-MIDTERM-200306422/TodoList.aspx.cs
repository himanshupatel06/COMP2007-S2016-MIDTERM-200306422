using COMP2007_S2016_MIDTERM_200306422.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COMP2007_S2016_MIDTERM_200306422
{
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // if loading the page for the first time, populate the  grid
            if (!IsPostBack)
            {
                Session["SortColumn"] = "TodoID"; // default sort column
                Session["SortDirection"] = "ASC";
                this.GetTodoList();
            }
        }

        protected void GetTodoList()
        {
            // connect
            using (TodoConnection db = new TodoConnection())
            {
                string SortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                var todoList = (from allList in db.Todos
                                select allList);

                // bind the result to the GridView
                TodoGridView.DataSource = todoList.ToList();
                TodoGridView.DataBind();
            }
        }

       

        protected void PageSizeDropDownList_SelectedIndexChanged1(object sender, EventArgs e)
        {
            // Set the new Page size
            TodoGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            // refresh the grid
            this.GetTodoList();
        }

        protected void TodoGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // store which row was clicked
            int selectedRow = e.RowIndex;

            
            int TodoID = Convert.ToInt32(TodoGridView.DataKeys[selectedRow].Values["TodoID"]);

            // use EF to find the selected Todo in the DB and remove it
            using (TodoConnection db = new TodoConnection())
            {
                
                Todo deletedTodo = (from TodoRecords in db.Todos
                                          where TodoRecords.TodoID ==TodoID
                                    select TodoRecords).FirstOrDefault();

                
                db.Todos.Remove(deletedTodo);

                // save my changes back to the database
                db.SaveChanges();

                // refresh the grid
                this.GetTodoList();
            }
        }

        protected void TodoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page number
            TodoGridView.PageIndex = e.NewPageIndex;

            // refresh the grid
            this.GetTodoList();
        }

        protected void TodoGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            // get the column to sorty by
            Session["SortColumn"] = e.SortExpression;

            // Refresh the Grid
            this.GetTodoList();

            // toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }

        protected void TodoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header) // if header row has been clicked
                {
                    LinkButton linkbutton = new LinkButton();

                    for (int index = 0; index < TodoGridView.Columns.Count - 1; index++)
                    {
                        if (TodoGridView.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "ASC")
                            {
                                linkbutton.Text = " <i class='fa fa-caret-up fa-lg'></i>";
                            }
                            else
                            {
                                linkbutton.Text = " <i class='fa fa-caret-down fa-lg'></i>";
                            }

                            e.Row.Cells[index].Controls.Add(linkbutton);
                        }
                    }
                }
            }
        }
    }
}