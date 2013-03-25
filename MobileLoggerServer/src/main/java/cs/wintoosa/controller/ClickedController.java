/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller;

import com.google.gson.JsonObject;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

/**
 *
 * @author jonimake
 */
@Controller
@RequestMapping("/log/clicked")
public class ClickedController {
    
    
    @RequestMapping(method= RequestMethod.PUT)
    @ResponseBody
    public boolean put(JsonObject json) {
        System.out.println(json.toString());
        return false;
    }
    
    @RequestMapping(method= RequestMethod.GET)
    @ResponseBody
    public boolean get() {
        return false;
    }
}
