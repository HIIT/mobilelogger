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
public class WeatherControllerTest extends AbstractTest {
    @Autowired
    private WebApplicationContext wac;
    private MockMvc mockMvc;

    @Before
    public void setup() {
        this.mockMvc = webAppContextSetup(this.wac).build();
        
    }
    
    @Test
    public void testPutJsonLog() throws Exception {
        
        String jsondata = "{\"temperature\":\"24\", "
                + "\"phoneId\":\"test\", "
                + "\"timestamp\":\"3\","
                + "\"windspeed\":\"4\","
                + "\"weatherdescription\":\"weatherdescriptionvalue\"}";

        this.mockMvc.perform(put("/log/weather").contentType(MediaType.APPLICATION_JSON).content(jsondata))
                .andExpect(status().isOk())
                .andExpect(content().string("true"))
                .andReturn();
    }
    
    @Test
    public void testGet() throws Exception {
        this.mockMvc.perform(get("/log/weather"))
                .andExpect(status().isOk())
                .andReturn();
    }
}