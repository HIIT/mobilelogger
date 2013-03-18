<%-- 
    Document   : session
    Created on : Mar 18, 2013, 1:29:34 PM
    Author     : jonimake
--%>

<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>JSP Page</title>
    </head>
    <body>
        <h1>Hello World!</h1>
        <ol>
            <c:forEach var="session" items="${sessions}">
                <li>
                    <a href="${ContextPath}/log/session/${session.id}">${session.id} - ${session.sessionStart} : ${session.sessionEnd}</a>
                </li>
            </c:forEach>
                
        </ol>
    </body>
</html>
