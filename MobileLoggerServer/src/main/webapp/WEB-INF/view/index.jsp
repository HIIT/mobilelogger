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
        <title>Mobile Logger Index</title>
    </head>
    <body>
        <h1>Mobile Logger Index</h1>
        <h2>${ContextPath}</h2>

        <h3>Done</h3>

        <ul>

            <li>
                <a href="<c:url value="/log/"/>" >All logs</a>
            </li>

            <li>
                <a href="<c:url value="/log/phone"/>" >Phones</a>
            </li>

            <li>
                <a href="<c:url value="/log/session"/>" >Sessions</a>
            </li>

            <li>
                <a href=" <c:url value="/log/gps"/>" >Gps</a>
            </li>

            <li>
                <a href="<c:url value="/log/compass"/>" >Compass</a>
            </li>

            <li>
                <a href="<c:url value="/log/accel"/>" >Acceleration</a>
            </li>

            <li>
                <a href="<c:url value="/log/gyro"/>" >Gyroscope</a>
            </li>

            <li>
                <a href="<c:url value="/log/network"/>" >Network</a>
            </li>

            <li>
                <a href="<c:url value="/log/keyboard"/>" >Keyboard</a>
            </li>

            <li>
                <a href="<c:url value="/log/keyPress"/>" >Keypress</a>
            </li>
            
            <li>
                <a href="<c:url value="/log/touch"/>" >Touch</a>
            </li>
            
            <li>
                <a href="<c:url value="/log/google"/>" >Search results</a>
            </li>
            <li>
                <a href="<c:url value="/log/clicked"/>" >Search result clicked</a>
            </li>
            <li>
                <a href="<c:url value="/log/weather"/>" >Weather</a>
            </li>

        </ul>

        <h3>Todo</h3>
        <ul>
        </ul>

        <a href="<c:url value="/log/session/add"/>" >seed db</a>
    </body>
</html>
