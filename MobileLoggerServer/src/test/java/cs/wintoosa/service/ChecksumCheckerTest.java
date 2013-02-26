/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.service;

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;

/**
 *
 * @author vkukkola
 */
public class ChecksumCheckerTest {
    
    public ChecksumCheckerTest() {
    }
    
    @BeforeClass
    public static void setUpClass() {
    }
    
    @AfterClass
    public static void tearDownClass() {
    }
    
    @Before
    public void setUp() {
    }
    
    @After
    public void tearDown() {
    }

    /**
     * Test of calcSHA1 method, of class ChecksumChecker.
     */
    @Test
    public void testCalcSHA1() {
        System.out.println("calcSHA1");
        
        //without escaping
        //{"lat":1.0,"lon":2.0,"alt":0.0,"phoneId":"123456789012345","timestamp":1361264436365}
        
        String src = "{\"lat\":1.0,\"lon\":2.0,\"alt\":0.0,\"phoneId\":\"123456789012345\",\"timestamp\":1361264436365}";
        String expResult = "2413a9ab3dc40a4a0de28316422f321c4bcd179a";
        String result = ChecksumChecker.calcSHA1(src);
        assertEquals(expResult, result);
    }
}
