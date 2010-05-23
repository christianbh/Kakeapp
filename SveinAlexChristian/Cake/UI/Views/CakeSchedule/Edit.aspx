<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Cake.UI.Models.Forms.HolidayEditForm>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
    $(document).ready(function () {
        $("#NewDate").datepicker({ dateFormat: 'dd.mm.yy' }); ;
    }); 
    </script>


    <%
        using (Html.BeginForm())
        {%>
        <%:Html.ValidationSummary(true)%>
        
        <fieldset>
            <legend>Endre</legend>
            
            <div class="editor-label">
                <%:Html.LabelFor(model => model.ExistingDate)%>
            </div>
            <div class="editor-field">
                <%:Model.ExistingDate.ToShortDateString()%>
                <%:Html.HiddenFor(model => model.ExistingDate, Model.ExistingDate.ToShortDateString())%>
            </div>
            
            <div class="editor-label">
                <%:Html.LabelFor(model => model.NewDate)%>
            </div>
            <div class="editor-field">
                <%:Html.TextBoxFor(model => model.NewDate, Model.NewDate.ToShortDateString())%>
                <%:Html.ValidationMessageFor(model => model.NewDate)%>
            </div>
            
            <p>
                <input type="submit" value="Lagre" />
            </p>
        </fieldset>

    <%
        }%>

    <div>
        <%:Html.ActionLink("Tilbake til listen", "Index")%>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

