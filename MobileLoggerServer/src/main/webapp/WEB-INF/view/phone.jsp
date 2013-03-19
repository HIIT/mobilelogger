<%-- 
    Document   : phones
    Created on : Mar 18, 2013, 11:20:47 AM
    Author     : jonimake
--%>

<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<%@page session="false" %>

<%@taglib uri="http://www.springframework.org/tags/form" prefix="form" %>
<%@taglib uri="http://www.springframework.org/tags" prefix="spring" %>
<%@taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<%@taglib uri="http://java.sun.com/jsp/jstl/fmt" prefix="fmt" %>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>JSP Page</title>
    </head>
    <body>
        <h1>Hello World!</h1>
        
        <ol>
            <c:forEach var="phone" items="${phones}">
                <li>
                    <a href="<c:url value="/log/phone/${phone.phoneId}"/>">${phone.phoneId}</a>
                </li>
            </c:forEach>
                
        </ol>
    </body>
</html>
