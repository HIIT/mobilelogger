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
public class GPSControllerTest extends AbstractTest{
    @Autowired
    private WebApplicationContext wac;
    private MockMvc mockMvc;

    @Before
    public void setup() {
        this.mockMvc = webAppContextSetup(this.wac).build();
        
    }
    
    @Test
    public void testPutGPSLog() throws Exception {
        GpsLog log = new GpsLog();
        log.setPhoneId(123456789012345l+"");
        log.setTimestamp(Long.MIN_VALUE);
        log.setAlt(Float.MAX_VALUE);
        log.setLat(Float.MAX_VALUE);
        log.setLon(Float.MAX_VALUE);
        log.setTimestamp(Long.MAX_VALUE);
        Gson gson = new Gson();
        this.mockMvc.perform(put("/log/gps").contentType(MediaType.APPLICATION_JSON).content(gson.toJson(log)))
                .andExpect(status().isOk())
                .andExpect(content().string("true"))
                .andReturn();
    }
    
    
}
