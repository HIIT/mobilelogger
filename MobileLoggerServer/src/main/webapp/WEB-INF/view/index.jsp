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
                <a href="<c:url value="/log/operator"/>" >Operator</a>
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
                <a href="<c:url value="/log/clicked"/>" >Clicked</a>
            </li>

            <li>
                <a href="<c:url value="/log/micro"/>" >Sounds</a>
            </li>

            <li>
                <a href="<c:url value="/log/light"/>" >Light</a>
            </li>

            <li>
                <a href="<c:url value="/log/wifi"/>" >Wifi</a>
            </li>

            <li>
                <a href="<c:url value="/log/proximity"/>" >Proximity</a>
            </li>

            <li>
                <a href="<c:url value="/log/bluetooth"/>" >Bluetooth</a>
            </li>

        </ul>
        <a href="<c:url value="/log/session/add"/>" >seed db</a>
    </body>
</html>
