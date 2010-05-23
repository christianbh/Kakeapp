<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Cake.Core.Domain.Department>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%
        Html.EnableClientValidation();%>
    <%
        using (Html.BeginForm())
        {%>
        <%:Html.ValidationSummary(true)%>

        <fieldset>
            <legend>Ny</legend>
           
            <div class="editor-label">
                <%:Html.LabelFor(model => model.ContactName)%>
            </div>
            <div class="editor-field">
                <%:Html.TextBoxFor(model => model.ContactName)%>
                <%:Html.ValidationMessageFor(model => model.ContactName)%>
            </div>
            
            <div class="editor-label">
                <%:Html.LabelFor(model => model.ContactEmail)%>
            </div>
            <div class="editor-field">
                <%:Html.TextBoxFor(model => model.ContactEmail)%>
                <%:Html.ValidationMessageFor(model => model.ContactEmail)%>
            </div>
                        
            <p>
                <input type="submit" value="Legg til" />
            </p>
        </fieldset>

    <%
        }%>

    <div>
        <%:Html.ActionLink("Tilbake til listen", "List")%>
    </div>
    <div>
        <%:Html.ActionLink("Tilbake til startsiden", "Index", "Home")%>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

