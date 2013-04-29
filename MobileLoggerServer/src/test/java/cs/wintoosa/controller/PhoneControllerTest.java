/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller;

import cs.wintoosa.AbstractTest;
import cs.wintoosa.domain.Sessionlog;
import cs.wintoosa.service.ILogService;
import org.junit.Before;
import org.junit.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.test.context.web.WebAppConfiguration;
import org.springframework.test.web.servlet.MockMvc;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;
import static org.springframework.test.web.servlet.setup.MockMvcBuilders.*;
import org.springframework.web.context.WebApplicationContext;

/**
 *
 * @author vkukkola
 */
@WebAppConfiguration
public class PhoneControllerTest extends AbstractTest{
    @Autowired
    private WebApplicationContext wac;
    private MockMvc mockMvc;
    @Autowired
    private ILogService logService;
    private Sessionlog session;
    
    @Before
    public void setup() {
        this.mockMvc = webAppContextSetup(this.wac).build();
        session = new Sessionlog();
        session.setPhoneId("test");
        session.setSessionEnd(new Long(2));
        session.setSessionStart(new Long(1));
    }
    
    @Test
    public void testListAll() throws Exception{
        logService.saveSessionLog(session);
        this.mockMvc.perform(get("/log/phone"))
                .andExpect(status().isOk())
                .andExpect(model().size(1))
                .andReturn();
    }
    
    @Test
    public void testGetByPhoneId() throws Exception{
        logService.saveSessionLog(session);
        this.mockMvc.perform(get("/log/phone/test"))
                .andExpect(status().isOk())
                .andExpect(model().size(1))
                .andReturn();
    }
}
