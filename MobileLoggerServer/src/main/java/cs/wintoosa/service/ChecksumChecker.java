/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.service;

import java.io.UnsupportedEncodingException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.logging.Level;
import java.util.logging.Logger;
import org.apache.commons.codec.binary.Hex;

/**
 *
 * @author vkukkola
 */
public class ChecksumChecker {
    public static String calcSHA1(String src){
        String checksum = "";
        if(src == null)
            return null;
        try{
            MessageDigest md = MessageDigest.getInstance("SHA-1");
            try{
                 md.update(src.getBytes("utf8"));
                 byte[] hash = md.digest();
                 checksum = new String(Hex.encodeHex(hash));
            }
            catch(UnsupportedEncodingException e){
                Logger.getLogger(ChecksumChecker.class.getName()).log(Level.SEVERE, e.toString());
            }
        }
        catch(NoSuchAlgorithmException e){
            Logger.getLogger(ChecksumChecker.class.getName()).log(Level.SEVERE, e.toString());
        }
        return checksum;
    }
}
