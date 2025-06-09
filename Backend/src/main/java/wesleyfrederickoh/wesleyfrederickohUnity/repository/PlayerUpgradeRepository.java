package wesleyfrederickoh.wesleyfrederickohUnity.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import wesleyfrederickoh.wesleyfrederickohUnity.model.Player;
import wesleyfrederickoh.wesleyfrederickohUnity.model.PlayerUpgrade;
import wesleyfrederickoh.wesleyfrederickohUnity.model.UpgradeType;

import java.util.List;
import java.util.Optional;
import java.util.UUID;

@Repository
public interface PlayerUpgradeRepository extends JpaRepository<PlayerUpgrade, UUID> {
    Optional<PlayerUpgrade> findByPlayerAndUpgradeType(Player player, UpgradeType upgradeType);
    List<PlayerUpgrade> findAllByPlayer(Player player);
}
