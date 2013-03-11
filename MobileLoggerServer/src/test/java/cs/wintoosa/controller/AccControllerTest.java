/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller;

import com.google.gson.Gson;
import cs.wintoosa.AbstractTest;
import cs.wintoosa.domain.GpsLog;
import cs.wintoosa.domain.Log;
import org.junit.Before;
import org.junit.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.test.context.web.WebAppConfiguration;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.web.context.WebApplicationContext;
import static org.springframework.test.web.servlet.setup.MockMvcBuilders.*;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;


/**
 *
 * @author vkukkola
 */
@WebAppConfiguration
public class AccControllerTest extends AbstractTest{
    @Autowired
    private WebApplicationContext wac;
    private MockMvc mockMvc;

    @Before
    public void setup() {
        this.mockMvc = webAppContextSetup(this.wac).build();
        
    }
    
    @Test
    public void testPutAccelerationLog() throws Exception {
        
        String jsondata = "{\"accX\":1.0,\"accY\":2.0,\"accZ\":0.0,\"phoneId\":\"123456789012345\",\"timestamp\":1361264436365,\"checksum\":\"2413a9ab3dc40a4a0de28316422f321c4bcd179a\"}";

        this.mockMvc.perform(put("/log/accel").contentType(MediaType.APPLICATION_JSON).content(jsondata))
                .andExpect(status().isOk())
                .andExpect(content().string("true"))
                .andReturn();
    }
    
    
}
