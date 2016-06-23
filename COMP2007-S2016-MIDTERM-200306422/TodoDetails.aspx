<%--
    Name: Himanshu Patel
    Student No: 200306422
    Date: 23-06-2016
    PageDetail: TodoDetails.aspx which display Details
    --%>
<%@ Page Title="Todo Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoDetails.aspx.cs" Inherits="COMP2007_S2016_MIDTERM_200306422.TodoDetails" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     
    <div class="container">
        <div class="row">
            <div class="col-md-offset-3 col-md-6">
                <h1>ToDO Details</h1>
                <h5>All Fields are Required</h5>
                <br />
                <div class="form-group">
                    <label class="control-label" for="TodoNameTextBox">ToDo Name</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="TodoNameTextBox" placeholder="Todo Name" required="true"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="control-label" for="NotesTextBox">ToDo Note</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="NotesTextBox" placeholder="Todo note" required="true"></asp:TextBox>
                </div>
                <div class="form-group">
                    
                    
                           <asp:CheckBox ID="Completed" runat="server" />
                         
                       
                    <label class="control-label" for="Completed">Completed</label>
                </div>
                <div class="text-right">
                    <asp:Button Text="Cancel" ID="CancelButton" CssClass="btn btn-warning btn-lg" runat="server" 
                        UseSubmitBehavior="false" CausesValidation="false" OnClick="CancelButton_Click" />
                    <asp:Button Text="Save" ID="SaveButton" CssClass="btn btn-primary btn-lg" runat="server" OnClick="SaveButton_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
