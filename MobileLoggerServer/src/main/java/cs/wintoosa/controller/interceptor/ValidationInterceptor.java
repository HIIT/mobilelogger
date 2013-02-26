
package cs.wintoosa.controller.interceptor;

import com.google.gson.*;
import cs.wintoosa.service.ChecksumChecker;
import java.util.Map.Entry;
import java.util.Set;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.springframework.web.servlet.ModelAndView;
import org.springframework.web.servlet.handler.HandlerInterceptorAdapter;
import java.util.logging.*;
import javax.servlet.http.HttpServletRequestWrapper;

/**
 *
 * @author jonimake
 */
public class ValidationInterceptor extends HandlerInterceptorAdapter {
    
    private final static Logger logger = Logger.getLogger(ValidationInterceptor.class.getName()); 

    @Override
    public boolean preHandle(HttpServletRequest request, HttpServletResponse response, Object handler) throws Exception {
        
        boolean isOk = super.preHandle(request, response, handler);
        
        if(!request.getMethod().equalsIgnoreCase("PUT"))
            return isOk; //only handle PUTs
       // HttpServletRequestWrapper wrapper = new HttpServletRequestWrapper(request);
        
        JsonObject json = convertToJsonObject(request);
        isOk = isValid(json);
        logger.info("isOk " + isOk);
        return isOk;
    }
    
    private JsonObject convertToJsonObject(HttpServletRequest request) {
        JsonObject json = null;
        JsonParser parser = new JsonParser();
        if(request.getQueryString() != null) {
            JsonElement parse = parser.parse(request.getQueryString());
            json = parse.getAsJsonObject();
            logger.info("json: " + json.toString());
        }
        return json;
    }
    
    private boolean isValid(JsonObject json) {
        
        if(json == null)
            return false;
        
        JsonElement checksumJson = json.get("checksum");
        
        if(checksumJson == null)
            return false;
        
        String checksum = checksumJson.getAsString();
        json.remove("checksum");
        String calculatedChecksum = ChecksumChecker.calcSHA1(json.toString());
        
        logger.info(checksum + " equals " + calculatedChecksum + ": " + checksum.equals(calculatedChecksum));
        
        return calculatedChecksum.equals(checksum);
    }
}
