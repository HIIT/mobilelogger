/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.service;

import java.security.MessageDigest;
import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;
import static org.mockito.Mockito.*;

/**
 *
 * @author vkukkola
 */
public class ChecksumCheckerTest {

    /**
     * Test of calcSHA1 method, of class ChecksumChecker.
     */
    @Test
    public void testCalcSHA1() throws Exception{
        System.out.println("testCalcSHA1");
        
        //without escaping
        //{"lat":1.0,"lon":2.0,"alt":0.0,"phoneId":"123456789012345","timestamp":1361264436365}
        
        String src = "{\"lat\":1.0,\"lon\":2.0,\"alt\":0.0,\"phoneId\":\"123456789012345\",\"timestamp\":1361264436365}";
        String expResult = "2413a9ab3dc40a4a0de28316422f321c4bcd179a";
        String result = ChecksumChecker.calcSHA1(src);
        assertEquals(expResult, result);
    }
    
    @Test
    public void testCalcSHA1Null() throws Exception{
        System.out.println("testCalcSHA1Null");
        
        String src = "{\"lat\":1.0,\"lon\":2.0,\"alt\":0.0,\"phoneId\":\"123456789012345\",\"timestamp\":1361264436365}";
        String expResult = "2413a9ab3dc40a4a0de28316422f321c4bcd179a";
        String result = ChecksumChecker.calcSHA1(null);
        assertEquals(null, result);
    }
}