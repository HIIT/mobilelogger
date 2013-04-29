/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.service;

import cs.wintoosa.AbstractTest;
import cs.wintoosa.domain.Acceleration;
import cs.wintoosa.domain.Compass;
import cs.wintoosa.domain.Gps;
import cs.wintoosa.domain.KeyPress;
import cs.wintoosa.domain.Keyboard;
import cs.wintoosa.domain.Abstractlog;
import cs.wintoosa.domain.Orientation;
import cs.wintoosa.domain.Sessionlog;
import cs.wintoosa.domain.Touch;
import cs.wintoosa.service.ISessionService.DataHolder;
import java.util.ArrayList;
import java.util.List;
import static org.junit.Assert.*;
import org.junit.Test;
import static org.mockito.Mockito.*;
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
        
        Sessionlog sessionLog = new Sessionlog();
        sessionLog.setPhoneId("1234");
        sessionLog.setSessionStart(0l);
        sessionLog.setSessionEnd(100l);
        
        ILogService mockLogService = mock(ILogService.class);
        
        when(mockLogService.getAllBySessionId(Acceleration.class, sessionLog)).thenReturn(null);
        when(mockLogService.getAllBySessionId(Compass.class, sessionLog)).thenReturn(null);
        when(mockLogService.getAllBySessionId(Keyboard.class, sessionLog)).thenReturn(null);
        when(mockLogService.getAllBySessionId(KeyPress.class, sessionLog)).thenReturn(null);
        when(mockLogService.getAllBySessionId(Orientation.class, sessionLog)).thenReturn(null);
        when(mockLogService.getAllBySessionId(Gps.class, sessionLog)).thenReturn(null);
        when(mockLogService.getAllBySessionId(Touch.class, sessionLog)).thenReturn(null);

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
        
        Sessionlog sessionLog = new Sessionlog();
        sessionLog.setPhoneId("1234");
        sessionLog.setSessionStart(0l);
        sessionLog.setSessionEnd(100l);
        
        ILogService mockLogService = mock(ILogService.class);
        
        List<Acceleration> acclogs = getMockList(Acceleration.class);
        List<Compass> complogs = getMockList(Compass.class);
        List<Keyboard> keyboards = getMockList(Keyboard.class);
        List<KeyPress> presses = getMockList(KeyPress.class);
        List<Orientation> orientations = getMockList(Orientation.class);
        List<Gps> gpslogs = getMockList(Gps.class);
        List<Touch> touches = getMockList(Touch.class);
        
        when(mockLogService.getAllBySessionId(Acceleration.class, sessionLog)).thenReturn(acclogs);
        when(mockLogService.getAllBySessionId(Compass.class, sessionLog)).thenReturn(complogs);
        when(mockLogService.getAllBySessionId(Keyboard.class, sessionLog)).thenReturn(keyboards);
        when(mockLogService.getAllBySessionId(KeyPress.class, sessionLog)).thenReturn(presses);
        when(mockLogService.getAllBySessionId(Orientation.class, sessionLog)).thenReturn(orientations);
        when(mockLogService.getAllBySessionId(Gps.class, sessionLog)).thenReturn(gpslogs);
        when(mockLogService.getAllBySessionId(Touch.class, sessionLog)).thenReturn(touches);

        SessionService instance = new SessionService();
        ReflectionTestUtils.setField(instance, "logService", mockLogService);
        
        DataHolder expResult = new DataHolder(sessionLog);
        DataHolder result = instance.formatForJsp(sessionLog);
        assertEquals(expResult.getSession().getPhoneId(), result.getSession().getPhoneId());
        assertEquals(expResult.getPhone(), result.getPhone());
    }
    
    private <T extends Abstractlog> List<T> getMockList(Class<T> cls) {
        List<T> logs = new ArrayList();
        for(int i = 0; i < 11; i++) {
            logs.add(mock(cls));
            Abstractlog log = logs.get(i);
            log.setTimestamp((long)i);
        }
        return logs;
    }
    
}
