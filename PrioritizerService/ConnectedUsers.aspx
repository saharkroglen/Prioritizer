<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConnectedUsers.aspx.cs" Inherits="PrioritizerService.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
    
    <table border=1 title="">
    <tr><td>User Name</td><td>Last ping</td></tr>
    <%if (PrioritizerService.Utils._usersDict == null) return;
      int numOfUsers = 0;
      foreach (var user in PrioritizerService.Utils.connectedUsers)
      {
          if (PrioritizerService.Utils._usersDict.ContainsKey(user.Key) && DateTime.Now.AddMinutes(-minutes) < user.Value.ToLocalTime())
          {
              numOfUsers++;
          %>
          <tr>
            <td><%=PrioritizerService.Utils._usersDict[user.Key].userName%></td>
            <td><%=user.Value.ToLocalTime() %></td>
          </tr>
          <%
          }
      }
       %>
        <tr></tr>
    </table>
    </br>
    Current Connected Users Count: <%= numOfUsers%>
    </div>
    </form>
</body>
</html>
