package cs.wintoosa.service;

import cs.wintoosa.domain.GpsLog;
import cs.wintoosa.repository.IGenericRepository;
import java.util.List;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

/**
 *
 * @author jonimake
 */
@Service
public class GenericLogService<T> implements IGenericLogService<T> {

    @Autowired
    private IGenericRepository<T> genericRepository;
    
    @Override
    @Transactional
    public List<T> getAll() {
        return genericRepository.findAll();
    }

    public IGenericRepository<T> getGenericRepository() {
        return genericRepository;
    }

    public void setGenericRepository(IGenericRepository<T> genericRepository) {
        this.genericRepository = genericRepository;
    }
    
}
