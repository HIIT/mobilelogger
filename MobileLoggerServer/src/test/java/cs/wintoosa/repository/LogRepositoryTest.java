package cs.wintoosa.repository;

import cs.wintoosa.AbstractTest;
import cs.wintoosa.domain.GpsLog;
import cs.wintoosa.domain.Log;
import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;
import org.springframework.beans.factory.annotation.Autowired;

/**
 *
 * @author jonimake
 */
public class LogRepositoryTest extends AbstractTest{
    
    @Autowired
    ILogRepository logRepository;
    
    @Test
    public void testSavePlainLog() throws Exception{

        Log log = new Log();
        log.setPhoneId(Long.MIN_VALUE);
        long timestampSeconds = System.currentTimeMillis()/1000;
        log.setTimestamp(timestampSeconds);
        
        Log save = logRepository.save(log);
        
        assertTrue(save.getId() != null);
        assertEquals("timestamp was incorrect", timestampSeconds, (long)save.getTimestamp());
        
    }
    
    @Test
    public void testSaveGpsLog() throws Exception{

        GpsLog log = new GpsLog();
        log.setPhoneId(Long.MIN_VALUE);
        long timestampSeconds = System.currentTimeMillis()/1000;
        log.setTimestamp(timestampSeconds);
        log.setLat(Float.MAX_VALUE);
        log.setLon(Float.MIN_VALUE);
        
        GpsLog save = logRepository.save(log);
        
        assertTrue(save.getId() != null);
        assertEquals("timestamp was incorrect", timestampSeconds, (long)save.getTimestamp());
        assertEquals("latitude was incorrect", Float.MAX_VALUE, (float)save.getLat(), 0);
        assertEquals("longnitude was incorrect", Float.MIN_VALUE, (float)save.getLon(), 0);
        
    }
    

}
