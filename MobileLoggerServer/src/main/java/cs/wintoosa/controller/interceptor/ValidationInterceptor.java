package cs.wintoosa.controller.interceptor;

import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;
import com.google.gson.stream.JsonReader;
import cs.wintoosa.service.ChecksumChecker;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.logging.*;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletRequestWrapper;
import javax.servlet.http.HttpServletResponse;
import org.springframework.web.servlet.handler.HandlerInterceptorAdapter;

/**
 *
 * @author jonimake
 */
public class ValidationInterceptor extends HandlerInterceptorAdapter {
    
    private static final Logger logger = Logger.getLogger(ValidationInterceptor.class .getName()); 
    public ValidationInterceptor() {
        super();
    }
    
    @Override
    public boolean preHandle(HttpServletRequest request, HttpServletResponse response, Object handler) throws Exception {
        
        //make a wrapper so we don't exhaust the inputstream before it reaches the controller
        HttpServletRequestWrapper requestWrapper = new HttpServletRequestWrapper(request); 
        
        boolean isOk = super.preHandle(request, response, handler);
        if(requestWrapper == null)
            return false;
        if(!requestWrapper.getMethod().equalsIgnoreCase("PUT"))
            return isOk; //only handle PUTs
        
        JsonObject json = convertToJsonObject(requestWrapper);
       
        if(!isValid(json)) {
            response.getWriter().write("json validation failed");
            if(json != null)
                logger.info("JSON:\n" + json.toString());
            return false;
        }
        return true;
    }
    
    private JsonObject convertToJsonObject(HttpServletRequest request) throws IOException {
        if(request.getContentLength() == -1) {
            return null;
        }
        byte[] buffer = new byte[request.getContentLength()];
        request.getInputStream().read(buffer, 0, request.getContentLength());
        String data = new String(buffer, "UTF-8");
        JsonParser parser = new JsonParser();
        JsonObject json = parser.parse(data).getAsJsonObject();
        return json;
    }
    
    private boolean isValid(JsonObject json) {
        if(json == null) {
            logger.info("json is null");
            return false;
        }
        
        JsonElement checksumJson = json.get("checksum");
        
        if(checksumJson == null) {
            logger.info("json checksum is null");
            return false;
        }
        
        String checksum = checksumJson.getAsString();
        json.remove("checksum");
        String calculatedChecksum = ChecksumChecker.calcSHA1(json.toString());
        if(!checksum.equalsIgnoreCase(calculatedChecksum)) {
            logger.info("Checksum validation failed\n"
                    + "\texpected:\t " + checksum
                    + "\n\tcalculated:\t " + calculatedChecksum);
            return false;
        }
        return true;
    }
}
