/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.repository;

import cs.wintoosa.AbstractTest;
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
    public void testSave() {
        Log log = new Log();
        long count = logRepository.count();
        
        logRepository.save(log);
        
        assertEquals("logrepository failed to save", count+1, logRepository.count());
        
    }
    

}
