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
public class SearchClickedControllerTest extends AbstractTest{
    @Autowired
    private WebApplicationContext wac;
    private MockMvc mockMvc;

    @Before
    public void setup() {
        this.mockMvc = webAppContextSetup(this.wac).build();
        
    }
    
    @Test
    public void testPutJsonLog() throws Exception {
        //      \"text\":\"{"
        
        
        
        String jsondata =  "{\"text\":\"http://www.google.fi\","
                + "\"type\":\"clicked\","
                + "\"timestamp\":1366978973268,"
                + "\"phoneId\":\"m79aRPLMZjWfTBfpFt4rMjgv1Eo=\","
                + "\"checksum\":\"09516865AB4ECBCA233BA2F8DF26894F2CF09C9F\"}";                
        //String jsondata =  "{\"text\":\"asd\",\"timestamp\":1366978973268,\"phoneId\":\"m79aRPLMZjWfTBfpFt4rMjgv1Eo=\",\"checksum\":\"09516865AB4ECBCA233BA2F8DF26894F2CF09C9F\"}";                
                
        this.mockMvc.perform(put("/log/clicked").contentType(MediaType.APPLICATION_JSON).content(jsondata))
                .andExpect(status().isOk())
                .andExpect(content().string("true"))
                .andReturn();
    }
    
    @Test
    public void testGet() throws Exception {
        this.mockMvc.perform(get("/log/clicked"))
                .andExpect(status().isOk())
                .andReturn();
    }
    
    
}
