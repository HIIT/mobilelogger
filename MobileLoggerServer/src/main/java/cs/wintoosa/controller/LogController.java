/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller;

import com.google.gson.Gson;
import cs.wintoosa.domain.Phone;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

/**
 *
 * @author jonimake
 */
@Controller
@RequestMapping(value = "/log")
public class LogController {
    
    @RequestMapping(method= RequestMethod.POST)
    @ResponseBody
    public String post() {
        
        System.out.println("POST lolololo");
        
        
        return "ok";
    }
    
    @RequestMapping(method= RequestMethod.PUT, consumes="application/json" )
    public String put() {
        
        System.out.println("PUT lolololo");
        
        
        return "ok";
    }
    
    @RequestMapping(method= RequestMethod.GET, produces="application/json" )
    @ResponseBody
    public Map get() {
        Map<String, List> map = new HashMap();
        List<String> dolan = new LinkedList();
        
        for(int i = 0; i < 10; i++)
            dolan.add("aksuly is dolan " + i);
        
        map.put("gooby plz", dolan);
        
        return map;
    }
    
}
