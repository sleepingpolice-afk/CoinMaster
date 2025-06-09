package wesleyfrederickoh.wesleyfrederickohUnity.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import wesleyfrederickoh.wesleyfrederickohUnity.model.UpgradeType;

import java.util.Optional;
import java.util.UUID;

@Repository
public interface UpgradeTypeRepository extends JpaRepository<UpgradeType, UUID> {
    Optional<UpgradeType> findByName(String name);
}
