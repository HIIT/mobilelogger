<%@page contentType="text/html;charset=UTF-8"%>
<%@page pageEncoding="UTF-8"%>
<%@page session="false" %>

<%@taglib uri="http://www.springframework.org/tags/form" prefix="form" %>
<%@taglib uri="http://www.springframework.org/tags" prefix="spring" %>
<%@taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<%@taglib uri="http://java.sun.com/jsp/jstl/fmt" prefix="fmt" %>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>JSP Page</title>
    </head>
    <body>
        <h1>Logs in session ${session.sessionId} of phone ${session.phone}</h1>
        <table border ="1">
            <tr>
                <th>Data Attribute</th>
                <c:forEach var="timestamp" items="${session.logData.foo}">
                    <th>${timestamp.key}</th>
                </c:forEach>
            </tr>

            <c:forEach var="log" items="${session.logData}">
                <tr>
                    <td>${log.key}</td>
                    <c:forEach var="entry" items="${log.value}">
                        <td>${entry.value}</td>
                    </c:forEach>
                </tr>
            </c:forEach>


        </table>
    </body>
</html>
