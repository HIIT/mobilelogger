package cs.wintoosa.controller;

import com.google.gson.Gson;
import cs.wintoosa.AbstractTest;
import cs.wintoosa.domain.GpsLog;
import cs.wintoosa.domain.Log;
import cs.wintoosa.domain.Phone;
import java.util.HashMap;
import java.util.Map;
import org.eclipse.persistence.config.ResultType;
import org.junit.Test;
import org.junit.Before;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;
import org.springframework.test.context.web.WebAppConfiguration;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.MvcResult;
import org.springframework.test.web.servlet.ResultActions;
import org.springframework.test.web.servlet.ResultHandler;
import org.springframework.web.context.WebApplicationContext;
import static org.springframework.test.web.servlet.setup.MockMvcBuilders.*;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;

@WebAppConfiguration
public class LogControllerTest extends AbstractTest {

    @Autowired
    private WebApplicationContext wac;
    private MockMvc mockMvc;

    @Before
    public void setup() {
        this.mockMvc = webAppContextSetup(this.wac).build();
    }

    @Test
    public void testPutPlainLog() throws Exception {
        System.out.println("testPutPlainLog");
        Log log = new Log();
        log.setPhoneId(123456789012345l);
        log.setTimestamp(Long.MIN_VALUE);
        Gson gson = new Gson();
        this.mockMvc.perform(put("/log").contentType(MediaType.APPLICATION_JSON).content(gson.toJson(log)))
                .andExpect(status().isOk())
                .andExpect(content().string("true"))
                .andReturn();
    }
/*
    @Test
    public void testPutGps() throws Exception {
        System.out.println("testPutGps");
        GpsLog log = new GpsLog();
        log.setPhoneId(123456789012345l);
        Gson gson = new Gson();
        log.setTimestamp(Long.MIN_VALUE);
        log.setLat(60.2f);
        log.setLon(24.9f);
        
        this.mockMvc.perform(put("/log/gps").contentType(MediaType.APPLICATION_JSON).content(gson.toJson(log)))
                .andExpect(status().isOk())
                .andExpect(content().string("true"))
                .andReturn();
    }
*/
}
