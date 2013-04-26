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
import org.eclipse.persistence.logging.LogFormatter;
import org.springframework.web.servlet.handler.HandlerInterceptorAdapter;

/**
 *
 * @author jonimake
 */
public class ValidationInterceptor extends HandlerInterceptorAdapter {
    
    private static final Logger logger = Logger.getLogger(ValidationInterceptor.class.getName()); 
    private static boolean handlerSet = false;
    public ValidationInterceptor() {
        super();
        if(!handlerSet) {
            handlerSet = true;
            try {
                FileHandler handler = new FileHandler(this.getClass().getSimpleName()+".log", true);
                handler.setLevel(Level.WARNING);
                handler.setFormatter(new LogFormatter());
                logger.addHandler(handler);
            } catch (IOException ex) {
                handlerSet = false;
                Logger.getLogger(ValidationInterceptor.class.getName()).log(Level.SEVERE, null, ex);
            } catch (SecurityException ex) {
                handlerSet = false;
                Logger.getLogger(ValidationInterceptor.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
    }
    
    @Override
    public boolean preHandle(final HttpServletRequest request, HttpServletResponse response, Object handler) throws Exception {
        boolean isOk = super.preHandle(request, response, handler);
        if(request == null)
            return false;
        
        //make a wrapper so we don't exhaust the inputstream before it reaches the controller
        HttpServletRequestWrapper requestWrapper = new HttpServletRequestWrapper(request);
        
        if(!requestWrapper.getMethod().equalsIgnoreCase("PUT"))
            return isOk; //only handle PUTs
        
        JsonObject json = convertToJsonObject(requestWrapper);
       
        if(!isValid(json)) {
            response.getWriter().write("json validation failed");
            return false;
        }
        return true;
    }
    
    private JsonObject convertToJsonObject(HttpServletRequest request) throws IOException {
        if(request.getContentLength() == -1) {
            return null;
        }
        byte[] buffer = new byte[request.getContentLength()];
        
        //InputStreamReader reader = new InputStreamReader(request.getInputStream(), "UTF-8");
        //reader.read(buffer);
        request.getInputStream().read(buffer, 0, request.getContentLength());
        String data = new String(buffer, "UTF-8");
        //String data = String.copyValueOf(buffer);
        JsonParser parser = new JsonParser();
        JsonObject json = parser.parse(data).getAsJsonObject();
        //reader.close();
        return json;
    }
    
    private boolean isValid(JsonObject json) {
        if(json == null) {
            return false;
        }
        String unspoiledJson = json.toString();
        JsonElement checksumJson = json.get("checksum");
        
        if(checksumJson == null) {
            return false;
        }
        
        String checksum = checksumJson.getAsString();
        json.remove("checksum");
        String calculatedChecksum = ChecksumChecker.calcSHA1(json.toString());
        if(!checksum.equalsIgnoreCase(calculatedChecksum)) {
            logger.log(Level.WARNING,"Checksum validation failed\n"
                    + "\tjson:    \t {0}" + "\n" 
                    + "\toriginal:\t {1}" + "\n" 
                    + "\texpected:\t {2}\n"
                    + "\tcalculated:\t {3}", new String[]{
                        json.toString(), 
                        unspoiledJson,
                        checksum, 
                        calculatedChecksum});
            return false;
        }
        return true;
    }
}
