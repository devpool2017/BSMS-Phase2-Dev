<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master" CodeBehind="OtherParameters.aspx.vb" Inherits="LBP.VS2010.BSMS.OtherParameters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../../Content/Scripts/PageScripts/Review/OtherParameters.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="well" style="padding-top: 0px;">
        <div class="form-group form-row" style="border-bottom: solid; border-width: 1px">
            <label class="col-form-label label-font-standard" style="font-size: 14px; color: #0db14b">
                Number of Days Prior to Current Date that can be Selected in Tagging Target Market as Leads
            </label>
        </div>
        <div class="form-row">
            <label class="col-3 col-form-label label-font-standard" style="font-size: 14px">
                Number of Days Before
            </label>
            <div id="divDaysBefore" class="col-3">
                <input type="text" id="txtDaysBefore" class="col-2 lbpControl form-control" autocomplete="off" disabled />
            </div>
        </div>
    </div>
    <div class="well" style="padding-top: 0px;">
        <div class="form-group form-row" style="border-bottom: solid; border-width: 1px">
            <label class="col-form-label label-font-standard" style="font-size: 14px; color: #0db14b">
                Bankwide Target Leads of Branch Heads
            </label>
        </div>
        <div class="form-row">
            <label class="col-3 col-form-label label-font-standard" style="font-size: 14px">
                Target Leads
            </label>
            <div id="divTargetLeads" class="col-3">
                <input type="text" id="txtTargetLeads" class="col-2 lbpControl form-control" autocomplete="off" disabled />
            </div>
        </div>
    </div>
    <div class="well" style="padding-top: 0px;">
        <div class="form-group form-row" style="border-bottom: solid; border-width: 1px">
            <label class="col-form-label label-font-standard" style="font-size: 14px; color: #0db14b">
                Bankwide Target New Accounts Closed
            </label>
        </div>
        <div class="form-row">
            <label class="col-3 col-form-label label-font-standard" style="font-size: 14px">
                Target Accounts Closed
            </label>
            <div id="divTargetAccounts" class="col-3">
                <input type="text" id="txtTargetAccountsClosed" class="col-2 lbpControl form-control" autocomplete="off" disabled />
            </div>
        </div>
    </div>
    <div class="well" style="padding-top: 0px;">
        <div class="form-group form-row" style="border-bottom: solid; border-width: 1px">
            <label class="col-form-label label-font-standard" style="font-size: 14px; color: #0db14b">
                Bankwide Amount of Target ADB
            </label>
        </div>
        <div class="form-row">
            <label class="col-3 col-form-label label-font-standard" style="font-size: 14px">
                Target ADB
            </label>
            <div id="divTargetADB" class="col-3">
                <input type="text" id="txtTargetADB" class="col-2 lbpControl form-control" autocomplete="off" disabled />
            </div>
        </div>
    </div>
    <div class="well" style="padding-top: 0px;">
        <div class="form-group form-row" style="border-bottom: solid; border-width: 1px">
            <label class="col-form-label label-font-standard" style="font-size: 14px; color: #0db14b">
                Number of Days Prior to Current Date that a Target Market can be Tagged/Untagged As Customer
            </label>
        </div>
        <div class="form-row">
            <label class="col-3 col-form-label label-font-standard" style="font-size: 14px">
                Number of Days Before
            </label>
            <div id="divDaysBefore2" class="col-3">
                <input type="text" id="txtDaysBefore2" class="col-2 lbpControl form-control" autocomplete="off" disabled />
            </div>
        </div>
    </div>
    <div class="well" style="padding-top: 0px;">
        <div class="form-group form-row" style="border-bottom: solid; border-width: 1px">
            <label class="col-form-label label-font-standard" style="font-size: 14px; color: #0db14b">
                Enables/Disables the User to Edit the Date of Visit
            </label>
        </div>
        <div class="form-row">
            <label class="col-3 col-form-label label-font-standard" style="font-size: 14px">
                Allow to Edit Date of Visit? (Y/N)
            </label>
            <div id="divEditDate" class="col-3">
                <input type="text" id="txtEditDate" class="col-2 lbpControl form-control" autocomplete="off" disabled />
            </div>
        </div>
    </div>
    <div class="well" style="padding-top: 0px;">
        <div class="form-group form-row" style="border-bottom: solid; border-width: 1px">
            <label class="col-form-label label-font-standard" style="font-size: 14px; color: #0db14b">
                Maximum Results in Search Target Market
            </label>
        </div>
        <div class="form-row">
            <label class="col-3 col-form-label label-font-standard" style="font-size: 14px">
                Maximum Search Results
            </label>
            <div id="divSearch" class="col-3">
                <input type="text" id="txtSearch" class="col-2 lbpControl form-control" autocomplete="off" disabled />
            </div>
        </div>
    </div>
    <div class="well" style="padding-top: 0px;">
        <div class="form-group form-row" style="border-bottom: solid; border-width: 1px">
            <label class="col-form-label label-font-standard" style="font-size: 14px; color: #0db14b">
                Potential Accounts Year
            </label>
        </div>
        <div class="form-row">
            <label class="col-3 col-form-label label-font-standard" style="font-size: 14px">
                Potential Accounts Year
            </label>
            <div id="divCPAYear" class="col-3">
                <input type="text" id="txtCPAYear" class="col-2 lbpControl form-control" autocomplete="off" disabled />
            </div>
        </div>
    </div>
    <div class="well" style="padding-top: 0px; padding-bottom: 0px;">
        <div class="form-group form-row" style="border-bottom: solid; border-width: 1px">
            <label class="col-form-label label-font-standard" style="font-size: 14px; color: #0db14b">
                Date Range for the input of Potential Accounts
            </label>
        </div>
        <div class="form-group form-row">
            <label class="col-3 col-form-label label-font-standard" style="font-size: 14px">
                Group
            </label>
            <div id="divRegion" class="col-5">
                  <select id="ddlRegion" class="form-select form-select-sm">
                        </select>
            </div>
            </div>
        <div class="form-group form-row">
                 <label class="col-3 col-form-label label-font-standard" style="font-size: 14px">
                Start Date Range
            </label>
            <div id="divStartDateRange" class="col-3">
                <input type="text" id="txtStartDateRange" class="col-2 lbpControl form-control" autocomplete="off" disabled />
            </div>
            </div>
         <div class="form-group form-row">
               <label class="col-3 col-form-label label-font-standard" style="font-size: 14px">
                End Date Range
            </label>
            <div id="divEndDateRange" class="col-3">
                <input type="text" id="txtEndDateRange" class="col-2 lbpControl form-control" autocomplete="off" disabled />
            </div>
        </div>
    </div>


</asp:Content>



























