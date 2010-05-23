<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Cake.UI.Models.Forms.HolidayEditForm>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Slette feriedag?
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>Slette?</legend>
        <div class="display-label">Feriedag</div>
        <div class="display-field"><%:Model.ExistingDate.ToShortDateString()%></div>
    </fieldset>
    <%
        using (Html.BeginForm())
        {%>
        <p>
		    <input type="submit" value="Slett" />
            <input type="hidden" value="<%:Model.ExistingDate.ToShortDateString()%>" name="existingDate" id="existingDate" />
		    <%:Html.ActionLink("Tilbake til listen", "Index")%>
        </p>
    <%
        }%>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

