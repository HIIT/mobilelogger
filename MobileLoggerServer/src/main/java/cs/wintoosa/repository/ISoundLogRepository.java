/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.repository;

import cs.wintoosa.domain.SoundLog;
import org.springframework.data.jpa.repository.JpaRepository;

/**
 *
 * @author vkukkola
 */
public interface ISoundLogRepository extends JpaRepository<SoundLog, Long>, ISoundLogRepositoryCustom{
    
}
