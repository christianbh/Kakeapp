<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Cake.Core.Domain.Department>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>Slette?</legend>
        <div class="display-label"><%:Html.LabelFor(model => model.ContactName)%></div>
        <div class="display-field"><%:Model.ContactName%></div>
        
        <div class="display-label"><%:Html.LabelFor(model => model.ContactEmail)%></div>
        <div class="display-field"><%:Model.ContactEmail%></div>
        
    </fieldset>
    <%
        using (Html.BeginForm())
        {%>
        <p>
		    <input type="submit" value="Slett" /> 
		    <%:Html.ActionLink("Tilbake", "List")%>
        </p>
    <%
        }%>

</asp:Content>

