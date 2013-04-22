/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller;

import cs.wintoosa.AbstractTest;
import org.junit.Before;
import org.junit.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
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
public class KeyboardControllerTest extends AbstractTest{
    @Autowired
    private WebApplicationContext wac;
    private MockMvc mockMvc;
    
    @Before
    public void setup() {
        this.mockMvc = webAppContextSetup(this.wac).build();
    }
    
    @Test
    public void testPut() throws Exception{
        String jsondata = "{\"phoneId\":\"test\", \"timestamp\":3, \"keyboardFocus\":\"test\"}";
        this.mockMvc.perform(put("/log/keyboard").contentType(MediaType.APPLICATION_JSON).content(jsondata))
                .andExpect(status().isOk())
                .andExpect(content().string("true"))
                .andReturn();
    }
    
    @Test
    public void testPutPress() throws Exception{
        String jsondata = "{\"phoneId\":\"test\", \"timestamp\":3, \"keyPressed\":\"test\"}";
        this.mockMvc.perform(put("/log/keyPress").contentType(MediaType.APPLICATION_JSON).content(jsondata))
                .andExpect(status().isOk())
                .andExpect(content().string("true"))
                .andReturn();
    }
    
    @Test
    public void testGetLogs() throws Exception{
        this.mockMvc.perform(get("/log/keyboard"))
                .andExpect(status().isOk())
                .andReturn();
    }
    
    @Test
    public void testGetKeypressLogs() throws Exception{
        this.mockMvc.perform(get("/log/keyPress"))
                .andExpect(status().isOk())
                .andReturn();
    }
}
