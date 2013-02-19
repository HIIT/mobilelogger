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
                <a href="${ContextPath}/MobileLoggerServerDev/log/">All logs</a>
            </li>
            <li>
                <a href="${ContextPath}/MobileLoggerServerDev/log/gps">GPS logs</a>
            </li>
            <li>
                <a href="${ContextPath}/MobileLoggerServerDev/log/sound">Soundspace logs</a>
            </li>
            <li>
                <a href="${ContextPath}/MobileLoggerServerDev/log/compass">Compass logs</a>
            </li>
            <li>
                <a href="${ContextPath}/MobileLoggerServerDev/log/acceleration">Acceleration logs</a>
            </li>
            <li>
                <a href="${ContextPath}/MobileLoggerServerDev/log/orientation">Gyroscope/Orientation logs</a>
            </li>
            <li>
                <a href="${ContextPath}/MobileLoggerServerDev/log/light">Environmental lighting intensity logs</a>
            </li>
            <li>
                <a href="${ContextPath}/MobileLoggerServerDev/log/wifi">Wifi logs</a>
            </li>
            <li>
                <a href="${ContextPath}/MobileLoggerServerDev/log/proximity">Display proximity sensor logs</a>
            </li>
            <li>
                <a href="${ContextPath}/MobileLoggerServerDev/log/bluetooth">Bluetooth logs</a>
            </li>
            <li>
                <a href="${ContextPath}/MobileLoggerServerDev/log/network">Mobile network logs</a>
            </li>
            <p>Add a new dummy GPS log</p>
            <form action="${ContextPath}/MobileLoggerServerDev/log/gps/put" method="put">
                <input type="submit" value="Add Dummy" >
            </form>
        </ul>
    
</body>
</html>
