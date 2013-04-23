/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller.interceptor;

import cs.wintoosa.AbstractTest;
import javax.servlet.ServletInputStream;
import javax.servlet.http.HttpServletRequest;
import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;
import static org.mockito.Mockito.*;
import org.springframework.mock.web.MockHttpServletRequest;
/**
 *
 * @author jonimake
 */
public class FilteredRequestTest extends AbstractTest{
    
    /**
     * Test of getInputStream method, of class FilteredRequest.
     */
    @Test
    public void testGetInputStream() throws Exception{
        System.out.println("getInputStream");
        
        MockHttpServletRequest request = new MockHttpServletRequest();
        byte[] content = new byte[]{1,2,3,4,5,6,7,8};
        request.setContent(content);
        
        FilteredRequest instance = new FilteredRequest(request);
        ServletInputStream result = instance.getInputStream();
        assertTrue(result != null);
    }
    
    @Test
    public void testGetNullInputStream() throws Exception{
        System.out.println("getNullInputStream");
        
        MockHttpServletRequest request = new MockHttpServletRequest();
        byte[] content = null;
        request.setContent(content);
        
        FilteredRequest instance = new FilteredRequest(request);
        ServletInputStream result = instance.getInputStream();
        assertTrue(result != null);
    }
    
    @Test
    public void testGetInputStreamIOException() throws Exception{
        System.out.println("getNullInputStream");
        
        MockHttpServletRequest request = new MockHttpServletRequest();
        byte[] content = new byte[]{1,2,3,4,5,6,7,8};
        request.setContent(content);
        request.getInputStream().close();
        
        FilteredRequest instance = new FilteredRequest(request);
        ServletInputStream result = instance.getInputStream();
        assertTrue(result != null);
    }
}
