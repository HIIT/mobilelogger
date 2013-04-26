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
public class NetworkControllerTest extends AbstractTest{
    @Autowired
    private WebApplicationContext wac;
    private MockMvc mockMvc;

    @Before
    public void setup() {
        this.mockMvc = webAppContextSetup(this.wac).build();
        
    }
    
    @Test
    public void testPutNetworkLog() throws Exception {
        String jsondata = "{\"operator\":\"FI SONERA\","
                + "\"timestamp\":1366978965645," 
                + "\"interfaceBandwidth\":\"480000\","
                + "\"interfaceCharacteristics\":\"None\","
                + "\"interfaceDescription\":\"DTPT over USB Serial\","
                + "\"interfaceName\":\"DTPT\","
                + "\"interfaceState\":\"Connected\","
                + "\"interfaceSubtype\":\"Desktop_PassThru\","
                + "\"interfaceType\":\"Ethernet\","
                + "\"phoneId\":\"m79aRPLMZjWfTBfpFt4rMjgv1Eo=\","
                + "\"checksum\":\"B9DC53F51AD86AFCD436C5B139DF90BB77EB73FA\"}";
        
        this.mockMvc.perform(put("/log/network").contentType(MediaType.APPLICATION_JSON).content(jsondata))
                .andExpect(status().isOk())
                .andExpect(content().string("true"))
                .andReturn();
    }
    
    @Test
    public void testGetLogs() throws Exception{
        this.mockMvc.perform(get("/log/network"))
                .andExpect(status().isOk())
                .andReturn();
    }
}
