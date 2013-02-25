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
        String src = "tamaonsha1hashi";
        String expResult = "f0e3fc8f9a3e3d27789482075293c7a6a3a24c06";
        String result = ChecksumChecker.calcSHA1(src);
        assertEquals(expResult, result);
    }
}
