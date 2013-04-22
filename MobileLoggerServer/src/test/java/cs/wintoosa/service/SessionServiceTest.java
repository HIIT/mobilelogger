/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.service;

import cs.wintoosa.AbstractTest;
import cs.wintoosa.domain.AccLog;
import cs.wintoosa.domain.CompLog;
import cs.wintoosa.domain.GpsLog;
import cs.wintoosa.domain.KeyPress;
import cs.wintoosa.domain.Keyboard;
import cs.wintoosa.domain.Log;
import cs.wintoosa.domain.OrientationLog;
import cs.wintoosa.domain.Phone;
import cs.wintoosa.domain.SessionLog;
import cs.wintoosa.domain.TouchLog;
import cs.wintoosa.repository.log.LogRepository;
import cs.wintoosa.repository.phone.PhoneRepository;
import cs.wintoosa.repository.session.SessionRepository;
import cs.wintoosa.service.ISessionService.DataHolder;
import java.util.ArrayList;
import java.util.List;
import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;
import static org.mockito.Mockito.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.test.util.ReflectionTestUtils;

/**
 *
 * @author jonimake
 */
public class SessionServiceTest extends AbstractTest{
    
    /**
     * Test of formatForJsp method, of class SessionService.
     */
    @Test
    public void testFormatForJsp() {
        System.out.println("testFormatForJsp");
        
        SessionLog sessionLog = new SessionLog();
        sessionLog.setPhoneId("1234");
        sessionLog.setSessionStart(0l);
        sessionLog.setSessionEnd(100l);
        
        ILogService mockLogService = mock(ILogService.class);
        
        when(mockLogService.getAllBySessionId(AccLog.class, sessionLog)).thenReturn(null);
        when(mockLogService.getAllBySessionId(CompLog.class, sessionLog)).thenReturn(null);
        when(mockLogService.getAllBySessionId(Keyboard.class, sessionLog)).thenReturn(null);
        when(mockLogService.getAllBySessionId(KeyPress.class, sessionLog)).thenReturn(null);
        when(mockLogService.getAllBySessionId(OrientationLog.class, sessionLog)).thenReturn(null);
        when(mockLogService.getAllBySessionId(GpsLog.class, sessionLog)).thenReturn(null);
        when(mockLogService.getAllBySessionId(TouchLog.class, sessionLog)).thenReturn(null);

        SessionService instance = new SessionService();
        ReflectionTestUtils.setField(instance, "logService", mockLogService);
        
        
        DataHolder expResult = new DataHolder(sessionLog);
        DataHolder result = instance.formatForJsp(sessionLog);
        assertEquals(expResult.getSession().getPhoneId(), result.getSession().getPhoneId());
        assertEquals(expResult.getPhone(), result.getPhone());
    }
    
    @Test
    public void testFormatForJsp2() {
        System.out.println("testFormatForJsp2");
        
        SessionLog sessionLog = new SessionLog();
        sessionLog.setPhoneId("1234");
        sessionLog.setSessionStart(0l);
        sessionLog.setSessionEnd(100l);
        
        ILogService mockLogService = mock(ILogService.class);
        
        List<AccLog> acclogs = getMockList(AccLog.class);
        List<CompLog> complogs = getMockList(CompLog.class);
        List<Keyboard> keyboards = getMockList(Keyboard.class);
        List<KeyPress> presses = getMockList(KeyPress.class);
        List<OrientationLog> orientations = getMockList(OrientationLog.class);
        List<GpsLog> gpslogs = getMockList(GpsLog.class);
        List<TouchLog> touches = getMockList(TouchLog.class);
        
        when(mockLogService.getAllBySessionId(AccLog.class, sessionLog)).thenReturn(acclogs);
        when(mockLogService.getAllBySessionId(CompLog.class, sessionLog)).thenReturn(complogs);
        when(mockLogService.getAllBySessionId(Keyboard.class, sessionLog)).thenReturn(keyboards);
        when(mockLogService.getAllBySessionId(KeyPress.class, sessionLog)).thenReturn(presses);
        when(mockLogService.getAllBySessionId(OrientationLog.class, sessionLog)).thenReturn(orientations);
        when(mockLogService.getAllBySessionId(GpsLog.class, sessionLog)).thenReturn(gpslogs);
        when(mockLogService.getAllBySessionId(TouchLog.class, sessionLog)).thenReturn(touches);

        SessionService instance = new SessionService();
        ReflectionTestUtils.setField(instance, "logService", mockLogService);
        
        DataHolder expResult = new DataHolder(sessionLog);
        DataHolder result = instance.formatForJsp(sessionLog);
        assertEquals(expResult.getSession().getPhoneId(), result.getSession().getPhoneId());
        assertEquals(expResult.getPhone(), result.getPhone());
    }
    
    private <T extends Log> List<T> getMockList(Class<T> cls) {
        List<T> logs = new ArrayList();
        for(int i = 0; i < 11; i++) {
            logs.add(mock(cls));
            Log log = logs.get(i);
            log.setTimestamp((long)i);
        }
        return logs;
    }
    
}
