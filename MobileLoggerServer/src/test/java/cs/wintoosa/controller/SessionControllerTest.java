/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller;

import cs.wintoosa.AbstractTest;
import cs.wintoosa.domain.SessionLog;
import cs.wintoosa.service.ILogService;
import org.junit.Before;
import org.junit.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.test.context.web.WebAppConfiguration;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.web.context.WebApplicationContext;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;
import static org.springframework.test.web.servlet.setup.MockMvcBuilders.*;
import org.springframework.util.Assert;

/**
 *
 * @author vkukkola
 */
@WebAppConfiguration
public class SessionControllerTest extends AbstractTest{
    @Autowired
    private WebApplicationContext wac;
    private MockMvc mockMvc;
    @Autowired
    private ILogService logService;

    private SessionLog session;
    
    @Before
    public void setup() {
        this.mockMvc = webAppContextSetup(this.wac).build();
        session = new SessionLog();
        session.setPhoneId("test");
        session.setSessionEnd(new Long(2));
        session.setSessionStart(new Long(1));
    }
    
    @Test
    public void testPutSessionLog() throws Exception {
        
        String jsondata = "{\"sessionStart\":\"3\", \"sessionEnd\":\"4\", \"phoneId\":\"test\"}";

        this.mockMvc.perform(put("/log/session").contentType(MediaType.APPLICATION_JSON).content(jsondata))
                .andExpect(status().isOk())
                .andReturn();
    }
    
    @Test
    public void testGet() throws Exception {
        
        this.mockMvc.perform(get("/log/session"))
                .andExpect(status().isOk())
                .andReturn();
    }
    
    @Test
    public void testGetLogsBySession() throws Exception {
        
        logService.saveSessionLog(session);
        SessionLog savedSession = logService.getSessionByPhoneId("test").get(0);
        Assert.notNull(savedSession);
        this.mockMvc.perform(get("/log/session/"+savedSession.getId()))
                .andExpect(status().isOk())
                .andReturn();
    }
    
    @Test
    public void testGetLogsBySessionMatrix() throws Exception {
        logService.saveSessionLog(session);
        SessionLog savedSession = logService.getSessionByPhoneId("test").get(0);
        Assert.notNull(savedSession);
        this.mockMvc.perform(get("/log/session/3/matrix"))
                .andExpect(status().isOk())
                .andExpect(content().string(""))
                .andReturn();
    }
    
}

