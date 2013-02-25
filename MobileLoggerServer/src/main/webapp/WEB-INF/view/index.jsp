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
                <a href="${ContextPath}/MobileLoggerServer/log/">All logs</a>
            </li>
            <li>
                <a href="${ContextPath}/MobileLoggerServer/log/gps">GPS logs</a>
            </li>
            <li>
                <a href="${ContextPath}/MobileLoggerServer/log/operator">Mobile network logs</a>
            </li>
            <li>
                <a href="${ContextPath}/MobileLoggerServer/log/compass">Compass logs</a>
            </li>
            <li>
                <a href="${ContextPath}/MobileLoggerServer/log/accel">Acceleration logs</a>
            </li>
            <li>
                <a href="${ContextPath}/MobileLoggerServer/log/gyro">Gyroscope/Orientation logs</a>
            </li>
            <li>
                <a href="${ContextPath}/MobileLoggerServer/log/micro">Soundspace logs</a>
            </li>
            <li>
                <a href="${ContextPath}/MobileLoggerServer/log/light">Environmental lighting intensity logs</a>
            </li>
            <li>
                <a href="${ContextPath}/MobileLoggerServer/log/wifi">Wifi logs</a>
            </li>
            <li>
                <a href="${ContextPath}/MobileLoggerServer/log/proximity">Display proximity sensor logs</a>
            </li>
            <li>
                <a href="${ContextPath}/MobileLoggerServer/log/bluetooth">Bluetooth logs</a>
            </li>
            
            <p>Add a new dummy GPS log</p>
            <form action="${ContextPath}/MobileLoggerServer/log/gps/put" method="put">
                <input type="submit" value="Add Dummy" >
            </form>
        </ul>
    
</body>
</html>
